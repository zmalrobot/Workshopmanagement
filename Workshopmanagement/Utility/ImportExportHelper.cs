using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility
{
    // ── DTOs ─────────────────────────────────────────────────────────────────

    public enum ImportMatchType { Insert, Update }

    public class ImportRowPreview
    {
        public int RowNumber { get; set; }
        public string[] Cells { get; set; } = Array.Empty<string>();
        public ImportMatchType MatchType { get; set; }
        public string? ExistingId { get; set; }
    }

    // ── Static helpers ────────────────────────────────────────────────────────

    public static class ImportExportHelper
    {
        // ── CSV I/O ──────────────────────────────────────────────────────────

        public static string BuildCsv(string[] headers, IEnumerable<string[]> dataRows)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(string.Join(",", headers.Select(CsvEscape)));
            foreach (var row in dataRows)
                sb.AppendLine(string.Join(",", row.Select(CsvEscape)));
            return sb.ToString();
        }

        private static string CsvEscape(string? value)
        {
            if (value == null) return string.Empty;
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }

        public static async Task<List<Dictionary<string, string>>> ParseCsvAsync(
            Microsoft.AspNetCore.Components.Forms.IBrowserFile file,
            string[] expectedHeaders)
        {
            using var ms = new MemoryStream();
            await using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            using var reader = new StreamReader(ms, System.Text.Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord   = true,
                TrimOptions       = TrimOptions.Trim,
                MissingFieldFound = null,
                BadDataFound      = null,
                HeaderValidated   = null,
            });

            var result = new List<Dictionary<string, string>>();
            await csv.ReadAsync();
            csv.ReadHeader();

            while (await csv.ReadAsync())
            {
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var h in expectedHeaders)
                    dict[h] = csv.TryGetField<string>(h, out var val) ? (val?.Trim() ?? string.Empty) : string.Empty;
                result.Add(dict);
            }

            return result;
        }

        // ── Customer ─────────────────────────────────────────────────────────

        public static Customer MapCustomer(Dictionary<string, string> row, Customer? existing)
        {
            var rawPw   = row.GetOr("Password");
            var password = string.IsNullOrWhiteSpace(rawPw)
                ? (existing?.Password ?? string.Empty)
                : Cypher.Encrypt(rawPw);

            var c = new Customer
            {
                Id              = existing?.Id ?? string.Empty,
                Name            = row.GetOr("Name"),
                Surname         = row.GetOr("Surname"),
                Username        = row.GetOr("Username"),
                Password        = password,
                BusinessName    = row.GetOr("BusinessName").NullIfEmpty(),
                PhonePrefix     = row.GetOr("PhonePrefix").NullIfEmpty(),
                PhoneNumber     = row.GetOr("PhoneNumber").NullIfEmpty(),
                Email           = row.GetOr("Email").NullIfEmpty(),
                AddressStreet   = row.GetOr("AddressStreet").NullIfEmpty(),
                AddressNumber   = row.GetOr("AddressNumber").NullIfEmpty(),
                AddressInterior = row.GetOr("AddressInterior").NullIfEmpty(),
                AddressCAP      = row.GetOr("AddressCAP").NullIfEmpty(),
                AddressCity     = row.GetOr("AddressCity").NullIfEmpty(),
                AddressProvince = row.GetOr("AddressProvince").NullIfEmpty(),
                VatNumber       = row.GetOr("VatNumber").NullIfEmpty(),
                FiscalCode      = row.GetOr("FiscalCode").NullIfEmpty(),
                IsCompany       = row.GetOr("IsCompany").ParseBool(),
                IsActive        = row.GetOr("IsActive").ParseBoolDefault(true),
                InsertDate      = existing?.InsertDate ?? DateTime.UtcNow,
                UpdateDate      = DateTime.UtcNow,
            };
            c.Sanitize();
            return c;
        }

        public static string[] CustomerToCells(Customer c)
        {
            return new[]
            {
                c.Name            ?? string.Empty,
                c.Surname         ?? string.Empty,
                c.Username        ?? string.Empty,
                string.Empty,                          // Password never exported
                c.BusinessName    ?? string.Empty,
                c.PhonePrefix     ?? string.Empty,
                c.PhoneNumber     ?? string.Empty,
                c.Email           ?? string.Empty,
                c.AddressStreet   ?? string.Empty,
                c.AddressNumber   ?? string.Empty,
                c.AddressInterior ?? string.Empty,
                c.AddressCAP      ?? string.Empty,
                c.AddressCity     ?? string.Empty,
                c.AddressProvince ?? string.Empty,
                c.VatNumber       ?? string.Empty,
                c.FiscalCode      ?? string.Empty,
                c.IsCompany.ToString(),
                c.IsActive.ToString(),
            };
        }

        // ── Vehicle ──────────────────────────────────────────────────────────

        public static Veichle MapVeichle(Dictionary<string, string> row, Veichle? existing, string? resolvedUserId)
        {
            var v = new Veichle
            {
                Id                          = existing?.Id ?? string.Empty,
                UserId                      = resolvedUserId ?? existing?.UserId ?? string.Empty,
                LicensePlate                = row.GetOr("LicensePlate"),
                Brand                       = row.GetOr("Brand").NullIfEmpty(),
                Model                       = row.GetOr("Model").NullIfEmpty(),
                FrameType                   = row.GetOr("FrameType").NullIfEmpty(),
                EngineType                  = row.GetOr("EngineType").NullIfEmpty(),
                FrameNumber                 = row.GetOr("FrameNumber").NullIfEmpty(),
                ActualKilometers            = row.GetOr("ActualKilometers").ParseIntNullable(),
                ActualFuelPercentage        = row.GetOr("ActualFuelPercentage").ParseIntNullable(),
                Note                        = row.GetOr("Note").NullIfEmpty(),
                RegistrationDate            = row.GetOr("RegistrationDate").ParseDateTimeOrNow(),
                InsertDate                  = existing?.InsertDate ?? DateTime.UtcNow,
                UpdateDate                  = DateTime.UtcNow,
                HasOpenWorkflow             = existing?.HasOpenWorkflow ?? false,
                OpenWorkflows               = existing?.OpenWorkflows ?? new List<OpenWorkflowEntry>(),
                WorkflowExecutionsCompleted = existing?.WorkflowExecutionsCompleted ?? new List<string>(),
            };
            v.Sanitize();
            return v;
        }

        public static string[] VeichleToCells(Veichle v)
        {
            return new[]
            {
                v.LicensePlate                ?? string.Empty,
                v.Brand                       ?? string.Empty,
                v.Model                       ?? string.Empty,
                v.FrameType                   ?? string.Empty,
                v.EngineType                  ?? string.Empty,
                v.FrameNumber                 ?? string.Empty,
                v.ActualKilometers?.ToString()     ?? string.Empty,
                v.ActualFuelPercentage?.ToString() ?? string.Empty,
                v.Note                        ?? string.Empty,
                v.RegistrationDate.ToString("yyyy-MM-dd"),
                string.Empty, // CustomerFiscalCode — not stored on vehicle
                string.Empty, // CustomerVatNumber
            };
        }

        // ── Component ────────────────────────────────────────────────────────

        public static Component MapComponent(Dictionary<string, string> row, Component? existing)
        {
            var c = new Component
            {
                Id                = existing?.Id ?? string.Empty,
                Barcode           = row.GetOr("Barcode").NullIfEmpty(),
                Name              = row.GetOr("Name"),
                Quanity           = row.GetOr("Quanity").ParseDecimal(),
                PricePerQuantity  = row.GetOr("PricePerQuantity").ParseDecimal(),
                UnitOfMeasurement = row.GetOr("UnitOfMeasurement"),
            };
            c.Sanitize();
            return c;
        }

        public static string[] ComponentToCells(Component c)
        {
            return new[]
            {
                c.Barcode           ?? string.Empty,
                c.Name              ?? string.Empty,
                c.Quanity.ToString(CultureInfo.InvariantCulture),
                c.PricePerQuantity.ToString(CultureInfo.InvariantCulture),
                c.UnitOfMeasurement ?? string.Empty,
            };
        }
    }

    // ── Extension helpers ─────────────────────────────────────────────────────

    public static class ImportExportStringExtensions
    {
        public static string GetOr(this Dictionary<string, string> dict, string key)
            => dict.TryGetValue(key, out var v) ? (v?.Trim() ?? string.Empty) : string.Empty;

        public static string? NullIfEmpty(this string s)
            => string.IsNullOrWhiteSpace(s) ? null : s;

        public static bool ParseBool(this string s)
            => s.Equals("true",  StringComparison.OrdinalIgnoreCase)
            || s.Equals("1",     StringComparison.OrdinalIgnoreCase)
            || s.Equals("si",    StringComparison.OrdinalIgnoreCase)
            || s.Equals("yes",   StringComparison.OrdinalIgnoreCase);

        public static bool ParseBoolDefault(this string s, bool defaultVal)
            => string.IsNullOrWhiteSpace(s) ? defaultVal : s.ParseBool();

        public static int? ParseIntNullable(this string s)
            => int.TryParse(s, out var v) ? v : null;

        public static decimal ParseDecimal(this string s)
            => decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var v) ? v : 0m;

        public static DateTime ParseDateTimeOrNow(this string s)
            => DateTime.TryParse(s, out var d) ? d : DateTime.UtcNow;
    }
}
