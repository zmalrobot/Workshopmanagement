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

    public class Customer : ISanitizable
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? BusinessName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhonePrefix { get; set; }
        public string? Email { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressNumber { get; set; }
        public string? AddressInterior { get; set; }
        public string? AddressCAP { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressProvince { get; set; }
        public string? VatNumber { get; set; }
        public string? FiscalCode { get; set; }
        public bool IsCompany { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }

        public void Sanitize()
        {
            Name             = Name?.Trim()!;
            Surname          = Surname?.Trim()!;
            Username         = Username?.Trim()!;
            Password         = Password?.Trim()!;
            BusinessName     = BusinessName?.Trim();
            PhoneNumber      = PhoneNumber?.Trim();
            PhonePrefix      = PhonePrefix?.Trim();
            Email            = Email?.Trim();
            AddressStreet    = AddressStreet?.Trim();
            AddressNumber    = AddressNumber?.Trim();
            AddressInterior  = AddressInterior?.Trim();
            AddressCAP       = AddressCAP?.Trim();
            AddressCity      = AddressCity?.Trim();
            AddressProvince  = AddressProvince?.Trim();
            VatNumber        = VatNumber?.Trim();
            FiscalCode       = FiscalCode?.Trim();
        }
    }
}
