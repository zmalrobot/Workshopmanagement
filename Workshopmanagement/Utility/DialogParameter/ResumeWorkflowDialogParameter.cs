using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility.DialogParameter
{
    public class ResumeWorkflowDialogParameter
    {
        public Employee employee = new Employee();
        public Customer customer = new Customer();
        public Veichle veichle = new Veichle();
        public Workflow workflow = new Workflow();
        public WorkflowTask workflowTask = new WorkflowTask();
        public WorkflowExecution workflowExecution = new WorkflowExecution();
        public List<Component> components = new List<Component>();
        public bool editOneExecution = false;  //In pratica apro in modalità edit singola esecuzione
        public bool viewOneExecution = false;  //Modalità visualizzazione
        public bool isNewExecution = false;    //Nuova esecuzione da creare (Duplica) - inserisce invece di aggiornare
    }
}
