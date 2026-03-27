using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility.DialogParameter
{
    public class EditScheduleDialogParameter
    {
        public string WorkflowExecutionSeriesId { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public int? EstimatedHours { get; set; }
        public int? EstimatedMinutes { get; set; }
    }
}
