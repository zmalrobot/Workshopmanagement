using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherComponent
    {
        public string? Id { get; set; }
        public string? Barcode { get; set; }
        public string? Name { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Component : ISanitizable
    {
        public string Id { get; set; } = null!;
        public string? Barcode { get; set; }
        public string Name { get; set; } = null!;
        public decimal Quanity { get; set; }
        public decimal PricePerQuantity { get; set; }
        public string UnitOfMeasurement { get; set; } = null!;

        public void Sanitize()
        {
            Barcode           = Barcode?.Trim();
            Name              = Name?.Trim()!;
            UnitOfMeasurement = UnitOfMeasurement?.Trim()!;
        }
    }
}
