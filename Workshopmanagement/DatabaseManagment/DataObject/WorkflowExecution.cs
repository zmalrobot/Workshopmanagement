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
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string UserId { get; set; }
        public string VeichleId { get; set; }
        public string WorkflowId { get; set; }
        public string WorkflowTaskId { get; set; }
        public string WorkflowExecutionSeriesId { get; set; } //Id della serie di esecuzione, tutti i task in questa serie hanno questo uguale, serve per raggrupparli
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

        //Disclaimer
        public bool IsDisclamerAleadySign { set; get; }
        public string DisclamierSign { get; set; }
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
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}
