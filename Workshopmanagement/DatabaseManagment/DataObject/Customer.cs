using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherCustomer
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? VatNumber { get; set; }
        public string? FiscalCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? InsertDateFrom { get; set; }
        public DateTime? InsertDateTo { get; set; }
    }

    public class Customer
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? BusinessName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhonePrefix { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? VatNumber { get; set; }
        public string? FiscalCode { get; set; }
        public bool IsCompany { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
