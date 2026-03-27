using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherWorkflow
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? InsertDateFrom { get; set; }
        public DateTime? InsertDateTo { get; set; }
    }

    public class Workflow
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public bool RequireDisclaimer { get; set; }
        public string Color { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? EstimatedHours { get; set; }
        public int? EstimatedMinutes { get; set; }
    }
}
