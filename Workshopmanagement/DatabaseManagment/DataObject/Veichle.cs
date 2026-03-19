using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherVeichle
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? LicensePlate { get; set; }
        public bool? HasOpenWorkflow { get; set; }
        public string? OpenWorkflowId { get; set; }
        public string? OpenWorkflowExecutionId { get; set; }
        public DateTime? InsertDateFrom { get; set; }
        public DateTime? InsertDateTo { get; set; }
    }

    public class Veichle
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string LicensePlate { get; set; }
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
        public string? OpenWorkflowId { get; set; }
        public string? OpenWorkflowTaskId { get; set; }
        public string? OpenWorkflowExecutionId { get; set; }
        public string? OpenWorkflowExecutionSeriesId { get; set; }
        public List<string> WorkflowExecutionsCompleted { get; set; }

    }
}
