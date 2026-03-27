using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessCompanySettings : DataAccessBase<CompanySettings, SearcherCompanySettings>
    {
        public static string CollectionName = "CompanySettings";

        #region Constructor
        public DataAccessCompanySettings(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<CompanySettings> FilterBuilder(SearcherCompanySettings searcher)
        {
            var builder = Builders<CompanySettings>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            return filter;
        }

        public override FilterDefinition<CompanySettings> FilterBuilder(string Id)
        {
            var builder = Builders<CompanySettings>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(CompanySettings table)
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
        public override UpdateDefinition<CompanySettings> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override CompanySettings GetDefaultIncrementObject(SearcherCompanySettings searcher)
        {
            return null!;
        }
        #endregion
    }
}
