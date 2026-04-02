using MongoDB.Driver;
using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using Workshopmanagement.Utility;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessAppLog : DataAccessBase<AppLog, SearcherAppLog>
    {
        public static string CollectionName = "AppLogs";

        private readonly IMongoCollection<AppLog> _collection;

        #region Constructor
        public DataAccessAppLog(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
            _collection = mongoDatabase.GetCollection<AppLog>(CollectionName);
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<AppLog> FilterBuilder(SearcherAppLog searcher)
        {
            var builder = Builders<AppLog>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Level))
                filter &= builder.Eq(x => x.Level, searcher.Level);

            if (searcher.DateFrom.HasValue)
                filter &= builder.Gte(x => x.Timestamp, Utility.ToUtc(searcher.DateFrom.Value));

            if (searcher.DateTo.HasValue)
                filter &= builder.Lte(x => x.Timestamp, Utility.ToUtc(searcher.DateTo.Value.AddDays(1).AddTicks(-1)));

            if (!string.IsNullOrWhiteSpace(searcher.EmployeeId))
                filter &= builder.Eq(x => x.EmployeeId, searcher.EmployeeId);

            if (!string.IsNullOrWhiteSpace(searcher.CustomerId))
                filter &= builder.Eq(x => x.CustomerId, searcher.CustomerId);

            if (!string.IsNullOrWhiteSpace(searcher.VeichleId))
                filter &= builder.Eq(x => x.VeichleId, searcher.VeichleId);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowId))
                filter &= builder.Eq(x => x.WorkflowId, searcher.WorkflowId);

            return filter;
        }

        public override FilterDefinition<AppLog> FilterBuilder(string id)
        {
            var builder = Builders<AppLog>.Filter;
            return builder.Eq(x => x.Id, id);
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(AppLog table)
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
        public override UpdateDefinition<AppLog> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }

        public override AppLog GetDefaultIncrementObject(SearcherAppLog searcher)
        {
            return null!;
        }
        #endregion

        #region Custom paged + sorted query
        /// <summary>
        /// Returns logs sorted newest-first with variable page size (used by MudDataGrid ServerData).
        /// </summary>
        public async Task<(List<AppLog> Results, bool IsLastPage)> FindPagedSortedAsync(
            SearcherAppLog searcher, int page, int pageSize)
        {
            var filter = FilterBuilder(searcher);

            var results = await _collection
                .Find(filter)
                .SortByDescending(x => x.Timestamp)
                .Skip(page * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var hasNextPage = await _collection
                .Find(filter)
                .Skip((page + 1) * pageSize)
                .Limit(1)
                .AnyAsync();

            return (results, !hasNextPage);
        }

        /// <summary>
        /// Returns total document count matching the searcher — needed for MudDataGrid TotalItems.
        /// </summary>
        public async Task<long> CountAsync(SearcherAppLog searcher)
        {
            var filter = FilterBuilder(searcher);
            return await _collection.CountDocumentsAsync(filter);
        }
        #endregion
    }
}
