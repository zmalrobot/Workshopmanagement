using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherTimeTracking
    {
        public string? Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? WorkflowExecutionSeriesId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TimeTracking
    {
        [BsonId]
        public string Id { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string WorkflowExecutionSeriesId { get; set; } = null!;
        public List<TimeTrackingSession> Sessions { get; set; } = new List<TimeTrackingSession>();
        public bool IsActive { get; set; }

        public TimeSpan GetTotalTime()
        {
            var total = TimeSpan.Zero;
            foreach (var session in Sessions)
            {
                var end = session.EndTime ?? DateTime.Now;
                total += end - session.StartTime;
            }
            return total;
        }
    }

    public class TimeTrackingSession
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
