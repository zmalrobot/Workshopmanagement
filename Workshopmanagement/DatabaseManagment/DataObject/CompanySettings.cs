using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherCompanySettings
    {
        public string? Id { get; set; }
    }

    public class CompanySettings : ISanitizable
    {
        [BsonId]
        public string Id { get; set; } = "default";

        // Company info
        public string? CompanyName { get; set; }
        public string? CompanyVatNumber { get; set; }
        public string? CompanyPhone { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressNumber { get; set; }
        public string? AddressInterior { get; set; }
        public string? AddressCAP { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressProvince { get; set; }
        public bool HasLogo { get; set; }

        // Appearance
        public string? SelectedTheme { get; set; }

        // Logging
        public string LogLevel { get; set; } = "Information";

        // Working hours
        public TimeOnly MorningStart { get; set; } = new TimeOnly(9, 0);
        public TimeOnly MorningEnd { get; set; } = new TimeOnly(13, 0);
        public TimeOnly AfternoonStart { get; set; } = new TimeOnly(14, 0);
        public TimeOnly AfternoonEnd { get; set; } = new TimeOnly(18, 0);

        public void Sanitize()
        {
            CompanyName      = CompanyName?.Trim();
            CompanyVatNumber = CompanyVatNumber?.Trim();
            CompanyPhone     = CompanyPhone?.Trim();
            AddressStreet    = AddressStreet?.Trim();
            AddressNumber    = AddressNumber?.Trim();
            AddressInterior  = AddressInterior?.Trim();
            AddressCAP       = AddressCAP?.Trim();
            AddressCity      = AddressCity?.Trim();
            AddressProvince  = AddressProvince?.Trim();
        }
    }
}
