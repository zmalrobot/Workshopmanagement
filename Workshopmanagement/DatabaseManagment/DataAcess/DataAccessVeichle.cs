using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessVeichle : DataAccessBase<Veichle, SearcherVeichle>
    {
        public static string CollectionName = "Veichles";

        #region Constructor
        public DataAccessVeichle(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<Veichle> FilterBuilder(SearcherVeichle searcher)
        {
            var builder = Builders<Veichle>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.UserId))
                filter &= builder.Eq(x => x.UserId, searcher.UserId);

            if (!string.IsNullOrWhiteSpace(searcher.LicensePlate))
                filter &= builder.Eq(x => x.LicensePlate, searcher.LicensePlate);

            if (searcher.HasOpenWorkflow.HasValue)
                filter &= builder.Eq(x => x.HasOpenWorkflow, searcher.HasOpenWorkflow.Value);

            if (searcher.InsertDateFrom.HasValue && searcher.InsertDateTo.HasValue)
                filter &= builder.And(builder.Gt(x => x.InsertDate, searcher.InsertDateFrom), builder.Lt(x => x.InsertDate, searcher.InsertDateTo));
            else if (searcher.InsertDateFrom.HasValue)
                filter &= builder.And(builder.Gt(x => x.InsertDate, searcher.InsertDateFrom));
            else if (searcher.InsertDateTo.HasValue)
                filter &= builder.And(builder.Lt(x => x.InsertDate, searcher.InsertDateTo));

            return filter;
        }

        public override FilterDefinition<Veichle> FilterBuilder(string Id)
        {
            var builder = Builders<Veichle>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(Veichle table)
        {
            return table.Id;
        }
        #endregion

        #region CanIncrement
        public override bool CanIncrement()
        {
            return false;
        }
        #endregion

        #region GetIncrementFilter
        public override UpdateDefinition<Veichle> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override Veichle GetDefaultIncrementObject(SearcherVeichle searcher)
        {
            return null!;
        }
        #endregion
    }
}
