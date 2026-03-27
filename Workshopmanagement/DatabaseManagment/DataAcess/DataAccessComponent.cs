using Workshopmanagement.DatabaseManagment.DataAcess.BaseComponents;
using Workshopmanagement.DatabaseManagment.DataObject;
using MongoDB.Driver;

namespace Workshopmanagement.DatabaseManagment.DataAcess
{
    public class DataAccessComponent : DataAccessBase<Component, SearcherComponent>
    {
        public static string CollectionName = "Components";

        #region Constructor
        public DataAccessComponent(IMongoDatabase mongoDatabase) : base(mongoDatabase, CollectionName)
        {
        }
        #endregion

        #region FilterBuilder
        public override FilterDefinition<Component> FilterBuilder(SearcherComponent searcher)
        {
            var builder = Builders<Component>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(searcher.Id))
                filter &= builder.Eq(x => x.Id, searcher.Id);

            if (!string.IsNullOrWhiteSpace(searcher.Barcode))
                filter &= builder.Eq(x => x.Barcode, searcher.Barcode);

            if (!string.IsNullOrWhiteSpace(searcher.Name))
                filter &= builder.Eq(x => x.Name, searcher.Name);

            return filter;
        }

        public override FilterDefinition<Component> FilterBuilder(string Id)
        {
            var builder = Builders<Component>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(Id))
                filter &= builder.Eq(x => x.Id, Id);

            return filter;
        }
        #endregion

        #region GetIdValue
        public override string GetIdValue(Component table)
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
        public override UpdateDefinition<Component> GetIncrementFilter(bool isIncrement)
        {
            return null!;
        }
        #endregion

        #region GetDefaultIncrementObject
        public override Component GetDefaultIncrementObject(SearcherComponent searcher)
        {
            return null!;
        }
        #endregion
    }
}
