using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public enum AppLogLevel
    {
        Debug = 0,
        Information = 1,
        Warning = 2,
        Error = 3
    }

    public class SearcherAppLog
    {
        public string? Level { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? EmployeeId { get; set; }
        public string? CustomerId { get; set; }
        public string? VeichleId { get; set; }
        public string? WorkflowId { get; set; }
    }

    public class AppLog : ISanitizable
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Level { get; set; } = AppLogLevel.Information.ToString();

        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }

        // Contextual fields — all nullable, populated only when relevant
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }

        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public string? VeichleId { get; set; }
        public string? VeichleLicensePlate { get; set; }

        public string? WorkflowId { get; set; }
        public string? WorkflowName { get; set; }

        public string? WorkflowExecutionId { get; set; }

        public string? WorkflowTaskId { get; set; }
        public string? WorkflowTaskName { get; set; }

        public string? AdditionalInfo { get; set; }

        public void Sanitize() { /* log messages are not trimmed */ }
    }
}
