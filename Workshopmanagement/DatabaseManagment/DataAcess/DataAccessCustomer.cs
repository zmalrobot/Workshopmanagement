using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessCustomer : DataAccessBase<Customer, SearcherCustomer>
    {
        public static string CollectionName = "Customers";

        #region Constructor
        public DataAccessCustomer(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<Customer> FilterBuilder(SearcherCustomer searcher)
        {
            var builder = Builders<Customer>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.Username))
                filter &= builder.Eq(x => x.Username, searcher.Username);

            if (!string.IsNullOrWhiteSpace(searcher.Password))
                filter &= builder.Eq(x => x.Password, searcher.Password);

            if (!string.IsNullOrWhiteSpace(searcher.Email))
                filter &= builder.Eq(x => x.Email, searcher.Email);

            if (!string.IsNullOrWhiteSpace(searcher.VatNumber))
                filter &= builder.Eq(x => x.VatNumber, searcher.VatNumber);

            if (!string.IsNullOrWhiteSpace(searcher.FiscalCode))
                filter &= builder.Eq(x => x.FiscalCode, searcher.FiscalCode);

            if (searcher.IsActive.HasValue)
                filter &= builder.Eq(x => x.IsActive, searcher.IsActive.Value);

            if (searcher.InsertDateFrom.HasValue && searcher.InsertDateTo.HasValue)
                filter &= builder.And(builder.Gt(x => x.InsertDate, searcher.InsertDateFrom), builder.Lt(x => x.InsertDate, searcher.InsertDateTo));
            else if (searcher.InsertDateFrom.HasValue)
                filter &= builder.And(builder.Gt(x => x.InsertDate, searcher.InsertDateFrom));
            else if (searcher.InsertDateTo.HasValue)
                filter &= builder.And(builder.Lt(x => x.InsertDate, searcher.InsertDateTo));

            return filter;
        }

        public override FilterDefinition<Customer> FilterBuilder(string Id)
        {
            var builder = Builders<Customer>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(Customer table)
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
        public override UpdateDefinition<Customer> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override Customer GetDefaultIncrementObject(SearcherCustomer searcher)
        {
            return null!;
        }
        #endregion
    }
}
