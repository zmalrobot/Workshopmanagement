using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessWorkflowExecution : DataAccessBase<WorkflowExecution, SearcherWorkflowExecution>
    {
        public static string CollectionName = "WorkflowExecutions";

        #region Constructor
        public DataAccessWorkflowExecution(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<WorkflowExecution> FilterBuilder(SearcherWorkflowExecution searcher)
        {
            var builder = Builders<WorkflowExecution>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.EmployeeId))
                filter &= builder.Eq(x => x.EmployeeId, searcher.EmployeeId);

            if (!string.IsNullOrWhiteSpace(searcher.UserId))
                filter &= builder.Eq(x => x.UserId, searcher.UserId);

            if (!string.IsNullOrWhiteSpace(searcher.VeichleId))
                filter &= builder.Eq(x => x.VeichleId, searcher.VeichleId);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowId))
                filter &= builder.Eq(x => x.WorkflowId, searcher.WorkflowId);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowTaskId))
                filter &= builder.Eq(x => x.WorkflowTaskId, searcher.WorkflowTaskId);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowExecutionId))
                filter &= builder.Eq(x => x.WorkflowExecutionSeriesId, searcher.WorkflowExecutionId);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowExecutionSeriesId))
                filter &= builder.Eq(x => x.WorkflowExecutionSeriesId, searcher.WorkflowExecutionSeriesId);

            return filter;
        }

        public override FilterDefinition<WorkflowExecution> FilterBuilder(string Id)
        {
            var builder = Builders<WorkflowExecution>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(WorkflowExecution table)
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
        public override UpdateDefinition<WorkflowExecution> GetIncrementFilter(bool isIncrement)
        {
            return null;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override WorkflowExecution GetDefaultIncrementObject(SearcherWorkflowExecution searcher)
        {
            return null;
        }
        #endregion
    }
}
