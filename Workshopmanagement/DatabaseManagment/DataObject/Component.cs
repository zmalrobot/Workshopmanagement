namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherComponent
    {
        public string? Id { get; set; }
        public string? Barcode { get; set; }
        public string? Name { get; set; }
    }

    public class Component
    {
        public string Id { get; set; }
        public string? Barcode { get; set; }
        public string Name { get; set; }
        public decimal Quanity { get; set; }
        public decimal PricePerQuantity { get; set; }
        public string UnitOfMeasurement { get; set; }
    }
}
