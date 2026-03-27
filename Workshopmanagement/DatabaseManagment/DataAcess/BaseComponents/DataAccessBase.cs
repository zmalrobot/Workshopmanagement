using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents
{
    public abstract class DataAccessBase<Table, Searcher> : IDataAccess<Table, Searcher> where Table : class where Searcher : class
    {
        private IMongoCollection<Table> collection;
        private string collectionName;

        public DataAccessBase(IMongoDatabase mongoDatabase, string collectionName)
        {
            collection = mongoDatabase.GetCollection<Table>(collectionName);
            this.collectionName = collectionName;
        }

        #region Insert
        public async Task InsertAsync(Table obj)
        {
            await collection.InsertOneAsync(obj);
        }

        public async Task InsertAsync(List<Table> objs)
        {
            await collection.InsertManyAsync(objs);
        }
        #endregion

        #region Update
        public async Task UpdateAsync(Table obj)
        {
            var id = GetIdValue(obj);
            var filter = FilterBuilder(id);

            await collection.ReplaceOneAsync(filter, obj);
        }
        #endregion

        #region Upsert
        public async Task UpsertAsync(Table obj, Searcher searcher)
        {
            var elements = await FindAsync(searcher);

            if (elements.Count() == 0)
            {
                await InsertAsync(obj);
            }
            else if (elements.Count() == 1)
            {
                await UpdateAsync(obj);
            }
            else
            {
                throw new Exception("Too many document found for the same searcher. Impossible to upsert");
            }
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(string id)
        {
            var filter = FilterBuilder(id);
            await collection.DeleteOneAsync(filter);
        }
        #endregion

        #region Find
        public async Task<Table?> FindByIdAsync(string Id)
        {
            var filter = FilterBuilder(Id);
            return (await collection.FindAsync(filter))?.FirstOrDefault();
        }

        public async Task<List<Table>> FindAsync(Searcher searcher)
        {
            var filter = FilterBuilder(searcher);
            return (await collection.FindAsync(filter))?.ToList() ?? new List<Table>();
        }

        public async Task<(List<Table> Results, bool IsLastPage)> FindPagedAsync(Searcher searcher, int page)
        {
            var size = 50;
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable($"{collectionName}PageSize")))
                size = Convert.ToInt32(Environment.GetEnvironmentVariable($"{collectionName}PageSize"));

            var skip = page * size; //0 -> 49  50 -> 99
            var limit = size;

            var filter = FilterBuilder(searcher);
            var result = (await collection.Find(filter).Skip(skip).Limit(limit).ToListAsync()) ?? new List<Table>();
            var isLastPage = !(collection.Find(filter).Skip((page + 1) * size).Limit(1).ToList().Count > 0);

            return (result, isLastPage);
        }
        #endregion

        #region IncrementValue
        public async Task IncrementCounterValueAsync(Searcher searcher, bool isIncrement, int retry = 0)
        {
            if (!CanIncrement())
                return;
            try
            {
                var elements = await FindAsync(searcher);

                if (elements.Count() == 0) //Crea il primo elemento con counter 0 se non esiste
                {

                    var obj = GetDefaultIncrementObject(searcher);
                    if (obj == null)
                        return;

                    await InsertAsync(obj);
                }

                var filter = FilterBuilder(searcher);
                var updateDef = GetIncrementFilter(isIncrement);

                if (elements.Count() == 1)
                {
                    await collection.UpdateOneAsync(filter, updateDef);
                }
                else
                {
                    await collection.UpdateManyAsync(filter, updateDef);
                }
            }
            catch (Exception ex) //Si verifica in caso di primo inserimento se viene spammato in parallelo. Brutto ma forse funziona.
            {
                if (ex is MongoWriteException && retry < 5)
                {
                    Thread.Sleep(retry * 100);
                    await IncrementCounterValueAsync(searcher, isIncrement, retry++);
                }
                else
                    throw;
            }
        }
        #endregion

        public abstract string GetIdValue(Table table);
        public abstract FilterDefinition<Table> FilterBuilder(Searcher searcher);
        public abstract FilterDefinition<Table> FilterBuilder(string Id);
        public abstract bool CanIncrement();
        public abstract UpdateDefinition<Table> GetIncrementFilter(bool isIncrement);
        public abstract Table GetDefaultIncrementObject(Searcher searcher);

    }
}
