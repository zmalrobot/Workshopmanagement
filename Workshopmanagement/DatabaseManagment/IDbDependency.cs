using Workshopmanagement.DatabaseManagment.DataAcess;

namespace Workshopmanagement.DatabaseManagment
{
    public interface IDbDependency
    {
        public DataAccessCustomer GetDataAccessCustomer();
        public DataAccessEmployee GetDataAccessEmployee();
        public DataAccessVeichle GetDataAccessVeichle();
        public DataAccessWorkflow GetDataAccessWorkflow();
        public DataAccessWorkflowTask GetDataAccessWorkflowTask();
        public DataAccessWorkflowExecution GetDataAccessWorkflowExecution();
        public DataAccessComponent GetDataAccessComponent();
        public DataAccessTimeTracking GetDataAccessTimeTracking();
        public DataAccessCompanySettings GetDataAccessCompanySettings();
        public DataAccessAppLog GetDataAccessAppLog();
    }
}
