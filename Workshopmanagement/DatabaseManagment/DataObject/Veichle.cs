using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherVeichle
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? LicensePlate { get; set; }
        public bool? HasOpenWorkflow { get; set; }
        public DateTime? InsertDateFrom { get; set; }
        public DateTime? InsertDateTo { get; set; }
    }

    public class OpenWorkflowEntry
    {
        public string WorkflowId { get; set; } = null!;
        public string WorkflowTaskId { get; set; } = null!;
        public string WorkflowExecutionId { get; set; } = null!;
        public string WorkflowExecutionSeriesId { get; set; } = null!;
    }

    [BsonIgnoreExtraElements]
    public class Veichle : ISanitizable
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? FrameType { get; set; }
        public string? EngineType { get; set; }
        public string? FrameNumber { get; set; }
        public int? ActualKilometers { get; set; }
        public int? ActualFuelPercentage { get; set; }
        public string? Note { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //WorkFlows
        public bool HasOpenWorkflow { get; set; }
        public List<OpenWorkflowEntry> OpenWorkflows { get; set; } = new List<OpenWorkflowEntry>();
        public List<string> WorkflowExecutionsCompleted { get; set; } = new List<string>();

        public void Sanitize()
        {
            LicensePlate = LicensePlate?.Trim()!;
            Brand        = Brand?.Trim();
            Model        = Model?.Trim();
            FrameType    = FrameType?.Trim();
            EngineType   = EngineType?.Trim();
            FrameNumber  = FrameNumber?.Trim();
            Note         = Note?.Trim();
        }
    }
}
