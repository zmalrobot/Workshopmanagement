using MongoDB.Bson.Serialization.Attributes;

namespace Workshopmanagement.DatabaseManagment.DataObject
{
    public class SearcherWorkflowExecution
    {
        public string? Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? UserId { get; set; }
        public string? VeichleId { get; set; }
        public string? WorkflowId { get; set; }
        public string? WorkflowTaskId { get; set; }
        public string? WorkflowExecutionId { get; set; }
        public string? WorkflowExecutionSeriesId { get; set; }
    }

    public class WorkflowExecution
    {
        public WorkflowExecution()
        {
            ExecutionStatus = WorkflowExecutionStatus.Todo;
            Executionoutcome = WorkflowExecutionoutcome.None;
            Images = new List<Image>();
            Components = new List<Component>();
        }

        [BsonId]
        public string Id { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string VeichleId { get; set; } = null!;
        public string WorkflowId { get; set; } = null!;
        public string WorkflowTaskId { get; set; } = null!;
        public string WorkflowExecutionSeriesId { get; set; } = null!; //Id della serie di esecuzione, tutti i task in questa serie hanno questo uguale, serve per raggrupparli
        public string? Titile { get; set; }
        public string? Note { get; set; }
        public int? KmPre { get; set; } //Salvo una copia per traccia
        public int? KmPost { get; set; }    //Salvo una copia per traccia
        public int? FuelPre { get; set; } //Salvo una copia per traccia
        public int? FuelPost { get; set; } //Salvo una copia per traccia
        public List<Image>? Images { get; set; }
        public List<Component>? Components { get; set; }
        public WorkflowExecutionStatus ExecutionStatus { get; set; }
        public WorkflowExecutionoutcome Executionoutcome { get; set; }
        public DateTime? OpenAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? StartDate { get; set; }
        public int? EstimatedHours { get; set; }
        public int? EstimatedMinutes { get; set; }
        public List<string>? AssignedEmployeeIds { get; set; }

        //Disclaimer
        public bool IsDisclamerAleadySign { set; get; }
        public string DisclamierSign { get; set; } = null!;
        public DateTime DisclamierSignDate { get; set; }
    }

    public enum WorkflowExecutionStatus
    {
        Todo,
        InProgress,
        Skipped,
        Closed
    }

    public enum WorkflowExecutionoutcome
    {
        None,
        Ok,
        RunningOut,
        ToBeVerified,
        ToBeReplaced,
        Verified,
        Replaced
    }

    public class Image
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string Path { get; set; } = null!;
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}
