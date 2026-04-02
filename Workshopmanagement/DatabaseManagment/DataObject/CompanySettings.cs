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
        }
    }
}
