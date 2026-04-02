using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;
using static Workshopmanagement.Utility.Utility;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessEmployee : DataAccessBase<Employee, SearcherEmployee>
    {
        public static string CollectionName = "Employees";

        #region Constructor
        public DataAccessEmployee(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<Employee> FilterBuilder(SearcherEmployee searcher)
        {
            var builder = Builders<Employee>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.Username))
                filter &= builder.Eq(x => x.Username, searcher.Username);

            if (!string.IsNullOrWhiteSpace(searcher.Password))
                filter &= builder.Eq(x => x.Password, searcher.Password);

            if (!string.IsNullOrWhiteSpace(searcher.Email))
                filter &= builder.Eq(x => x.Email, searcher.Email);

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

        public override FilterDefinition<Employee> FilterBuilder(string Id)
        {
            var builder = Builders<Employee>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(Employee table)
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
        public override UpdateDefinition<Employee> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override Employee GetDefaultIncrementObject(SearcherEmployee searcher)
        {
            return null!;
        }
        #endregion
    }
}
