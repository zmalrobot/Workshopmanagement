using Workshopmanagement.DatabaseManagment.DataAcess;

namespace Workshopmanagement.DatabaseManagment
{
    public class DbDependency : IDbDependency
    {
        private MongoDB.Driver.IMongoDatabase db;

        private DataAccessCustomer dataAccessCustomer;
        private DataAccessEmployee dataAccessEmployee;
        private DataAccessVeichle  dataAccessVeichle;
        private DataAccessWorkflow dataAccessWorkflow;
        private DataAccessWorkflowTask dataAccessWorkflowTask;
        private DataAccessWorkflowExecution dataAccessWorkflowExecution;
        private DataAccessComponent dataAccessComponent;
        private DataAccessTimeTracking dataAccessTimeTracking;

        public DbDependency()
        {
            db = MongoDbEngine.GetDatabase();
            dataAccessCustomer = new DataAccessCustomer(db);
            dataAccessEmployee = new DataAccessEmployee(db);
            dataAccessVeichle = new DataAccessVeichle(db);
            dataAccessWorkflow = new DataAccessWorkflow(db);
            dataAccessWorkflowTask = new DataAccessWorkflowTask(db);
            dataAccessWorkflowExecution = new DataAccessWorkflowExecution(db);
            dataAccessComponent = new DataAccessComponent(db);
            dataAccessTimeTracking = new DataAccessTimeTracking(db);
        }

        public DataAccessCustomer GetDataAccessCustomer()
        {
            return dataAccessCustomer;
        }

        public DataAccessEmployee GetDataAccessEmployee()
        {
            return dataAccessEmployee;
        }

        public DataAccessVeichle GetDataAccessVeichle()
        {
            return dataAccessVeichle;
        }

        public DataAccessWorkflow GetDataAccessWorkflow()
        {
            return dataAccessWorkflow;
        }

        public DataAccessWorkflowTask GetDataAccessWorkflowTask()
        {
            return dataAccessWorkflowTask;
        }

        public DataAccessWorkflowExecution GetDataAccessWorkflowExecution()
        {
            return dataAccessWorkflowExecution;
        }

        public DataAccessComponent GetDataAccessComponent()
        {
            return dataAccessComponent;
        }

        public DataAccessTimeTracking GetDataAccessTimeTracking()
        {
            return dataAccessTimeTracking;
        }

    }
}
