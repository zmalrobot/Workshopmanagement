using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessWorkflowTask : DataAccessBase<WorkflowTask, SearcherWorkflowTask>
    {
        public static string CollectionName = "WorkflowTasks";

        #region Constructor
        public DataAccessWorkflowTask(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<WorkflowTask> FilterBuilder(SearcherWorkflowTask searcher)
        {
            var builder = Builders<WorkflowTask>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.WorkflowId))
                filter &= builder.Eq(x => x.WorkflowId, searcher.WorkflowId);

            if (!string.IsNullOrWhiteSpace(searcher.Title))
                filter &= builder.Eq(x => x.Title, searcher.Title);

            return filter;
        }

        public override FilterDefinition<WorkflowTask> FilterBuilder(string Id)
        {
            var builder = Builders<WorkflowTask>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(WorkflowTask table)
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
        public override UpdateDefinition<WorkflowTask> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override WorkflowTask GetDefaultIncrementObject(SearcherWorkflowTask searcher)
        {
            return null!;
        }
        #endregion
    }
}
