using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;
using static Workshopmanagement.Utility.Utility;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessWorkflow : DataAccessBase<Workflow, SearcherWorkflow>
    {
        public static string CollectionName = "Workflows";

        #region Constructor
        public DataAccessWorkflow(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<Workflow> FilterBuilder(SearcherWorkflow searcher)
        {
            var builder = Builders<Workflow>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.Title))
                filter &= builder.Eq(x => x.Title, searcher.Title);

            if (searcher.IsActive.HasValue)
                filter &= builder.Eq(x => x.IsActive, searcher.IsActive.Value);

            if (searcher.InsertDateFrom.HasValue && searcher.InsertDateTo.HasValue)
                filter &= builder.And(builder.Gt(x => x.InsertDate, ToUtc(searcher.InsertDateFrom.Value)), builder.Lt(x => x.InsertDate, ToUtc(searcher.InsertDateTo.Value)));
            else if (searcher.InsertDateFrom.HasValue)
                filter &= builder.And(builder.Gt(x => x.InsertDate, ToUtc(searcher.InsertDateFrom.Value)));
            else if (searcher.InsertDateTo.HasValue)
                filter &= builder.And(builder.Lt(x => x.InsertDate, ToUtc(searcher.InsertDateTo.Value)));

            return filter;
        }

        public override FilterDefinition<Workflow> FilterBuilder(string Id)
        {
            var builder = Builders<Workflow>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(Workflow table)
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
        public override UpdateDefinition<Workflow> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override Workflow GetDefaultIncrementObject(SearcherWorkflow searcher)
        {
            return null!;
        }
        #endregion
    }
}
