using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility.DialogParameter
{
    public class StartWorkflowDialogParameter
    {
        public Employee employee = new Employee();
        public List<Customer> customers = new List<Customer>();
        public List<Veichle> veichles = new List<Veichle>();
        public List<Workflow> workflows = new List<Workflow>();
        public List<WorkflowTask> workflowTasks = new List<WorkflowTask>();
    }
}
