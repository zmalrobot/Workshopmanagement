using MongoDB.Driver;
using Workshopmanagement.DatabaseManagment.DataAcess;
using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.DatabaseManagment
{
    public static class MongoDbIndexInitializer
    {
        public static async Task EnsureIndexesAsync()
        {
            var db = MongoDbEngine.GetDatabase();

            await EnsureCustomerIndexesAsync(db);
            await EnsureEmployeeIndexesAsync(db);
            await EnsureVeichleIndexesAsync(db);
            await EnsureWorkflowIndexesAsync(db);
            await EnsureWorkflowTaskIndexesAsync(db);
            await EnsureWorkflowExecutionIndexesAsync(db);
            await EnsureTimeTrackingIndexesAsync(db);
            await EnsureComponentIndexesAsync(db);
            await EnsureAppLogIndexesAsync(db);
        }

        private static async Task EnsureCustomerIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<Customer>(DataAccessCustomer.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Customer>(
                Builders<Customer>.IndexKeys.Ascending(x => x.Username),
                new CreateIndexOptions { Unique = true, Name = "idx_customers_username_unique" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Customer>(
                Builders<Customer>.IndexKeys.Ascending(x => x.Email),
                new CreateIndexOptions { Sparse = true, Name = "idx_customers_email" }));
        }

        private static async Task EnsureEmployeeIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<Employee>(DataAccessEmployee.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Employee>(
                Builders<Employee>.IndexKeys.Ascending(x => x.Username),
                new CreateIndexOptions { Unique = true, Name = "idx_employees_username_unique" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Employee>(
                Builders<Employee>.IndexKeys.Ascending(x => x.IsActive),
                new CreateIndexOptions { Name = "idx_employees_isactive" }));
        }

        private static async Task EnsureVeichleIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<Veichle>(DataAccessVeichle.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Veichle>(
                Builders<Veichle>.IndexKeys.Ascending(x => x.UserId),
                new CreateIndexOptions { Name = "idx_veichles_userid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Veichle>(
                Builders<Veichle>.IndexKeys.Ascending(x => x.LicensePlate),
                new CreateIndexOptions { Name = "idx_veichles_licenseplate" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Veichle>(
                Builders<Veichle>.IndexKeys.Ascending(x => x.HasOpenWorkflow),
                new CreateIndexOptions { Name = "idx_veichles_hasopenworkflow" }));
        }

        private static async Task EnsureWorkflowIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<Workflow>(DataAccessWorkflow.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Workflow>(
                Builders<Workflow>.IndexKeys.Ascending(x => x.IsActive),
                new CreateIndexOptions { Name = "idx_workflows_isactive" }));
        }

        private static async Task EnsureWorkflowTaskIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<WorkflowTask>(DataAccessWorkflowTask.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<WorkflowTask>(
                Builders<WorkflowTask>.IndexKeys
                    .Ascending(x => x.WorkflowId)
                    .Ascending(x => x.TaskOrder),
                new CreateIndexOptions { Name = "idx_workflowtasks_workflowid_taskorder" }));
        }

        private static async Task EnsureWorkflowExecutionIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<WorkflowExecution>(DataAccessWorkflowExecution.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<WorkflowExecution>(
                Builders<WorkflowExecution>.IndexKeys.Ascending(x => x.WorkflowExecutionSeriesId),
                new CreateIndexOptions { Name = "idx_wfexecutions_seriesid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<WorkflowExecution>(
                Builders<WorkflowExecution>.IndexKeys.Ascending(x => x.VeichleId),
                new CreateIndexOptions { Name = "idx_wfexecutions_veichleid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<WorkflowExecution>(
                Builders<WorkflowExecution>.IndexKeys.Ascending(x => x.EmployeeId),
                new CreateIndexOptions { Name = "idx_wfexecutions_employeeid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<WorkflowExecution>(
                Builders<WorkflowExecution>.IndexKeys
                    .Ascending(x => x.UserId)
                    .Ascending(x => x.IsCompleted),
                new CreateIndexOptions { Name = "idx_wfexecutions_userid_iscompleted" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<WorkflowExecution>(
                Builders<WorkflowExecution>.IndexKeys
                    .Ascending(x => x.IsCompleted)
                    .Ascending(x => x.WorkflowId),
                new CreateIndexOptions { Name = "idx_wfexecutions_iscompleted_workflowid" }));
        }

        private static async Task EnsureTimeTrackingIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<TimeTracking>(DataAccessTimeTracking.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<TimeTracking>(
                Builders<TimeTracking>.IndexKeys.Ascending(x => x.WorkflowExecutionSeriesId),
                new CreateIndexOptions { Name = "idx_timetracking_seriesid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<TimeTracking>(
                Builders<TimeTracking>.IndexKeys
                    .Ascending(x => x.EmployeeId)
                    .Ascending(x => x.IsActive),
                new CreateIndexOptions { Name = "idx_timetracking_employeeid_isactive" }));
        }

        private static async Task EnsureComponentIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<Component>(DataAccessComponent.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Component>(
                Builders<Component>.IndexKeys.Ascending(x => x.Barcode),
                new CreateIndexOptions { Sparse = true, Name = "idx_components_barcode" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<Component>(
                Builders<Component>.IndexKeys.Ascending(x => x.Name),
                new CreateIndexOptions { Name = "idx_components_name" }));
        }

        private static async Task EnsureAppLogIndexesAsync(IMongoDatabase db)
        {
            var col = db.GetCollection<AppLog>(DataAccessAppLog.CollectionName);

            await col.Indexes.CreateOneAsync(new CreateIndexModel<AppLog>(
                Builders<AppLog>.IndexKeys.Descending(x => x.Timestamp),
                new CreateIndexOptions { Name = "idx_applogs_timestamp" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<AppLog>(
                Builders<AppLog>.IndexKeys.Ascending(x => x.Level),
                new CreateIndexOptions { Name = "idx_applogs_level" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<AppLog>(
                Builders<AppLog>.IndexKeys
                    .Descending(x => x.Timestamp)
                    .Ascending(x => x.Level),
                new CreateIndexOptions { Name = "idx_applogs_timestamp_level" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<AppLog>(
                Builders<AppLog>.IndexKeys.Ascending(x => x.EmployeeId),
                new CreateIndexOptions { Sparse = true, Name = "idx_applogs_employeeid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<AppLog>(
                Builders<AppLog>.IndexKeys.Ascending(x => x.CustomerId),
                new CreateIndexOptions { Sparse = true, Name = "idx_applogs_customerid" }));

            await col.Indexes.CreateOneAsync(new CreateIndexModel<AppLog>(
                Builders<AppLog>.IndexKeys.Ascending(x => x.VeichleId),
                new CreateIndexOptions { Sparse = true, Name = "idx_applogs_veicleid" }));
        }
    }
}
