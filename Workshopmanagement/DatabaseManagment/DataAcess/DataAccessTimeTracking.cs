using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessTimeTracking : DataAccessBase<TimeTracking, SearcherTimeTracking>
    {
        public static string CollectionName = "TimeTrackings";

        #region Constructor
        public DataAccessTimeTracking(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<TimeTracking> FilterBuilder(SearcherTimeTracking searcher)
        {
            var builder = Builders<TimeTracking>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.EmployeeId))
                filter &= builder.Eq(x => x.EmployeeId, searcher.EmployeeId);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowExecutionSeriesId))
                filter &= builder.Eq(x => x.WorkflowExecutionSeriesId, searcher.WorkflowExecutionSeriesId);

            if (searcher.IsActive.HasValue)
                filter &= builder.Eq(x => x.IsActive, searcher.IsActive.Value);

            return filter;
        }

        public override FilterDefinition<TimeTracking> FilterBuilder(string Id)
        {
            var builder = Builders<TimeTracking>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(TimeTracking table)
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
        public override UpdateDefinition<TimeTracking> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override TimeTracking GetDefaultIncrementObject(SearcherTimeTracking searcher)
        {
            return null!;
        }
        #endregion
    }
}
