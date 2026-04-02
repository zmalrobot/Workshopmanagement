using Workshopmanagement.DatabaseManagment;
using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility
{
    public interface IAppLogger
    {
        Task LogAsync(AppLogLevel level, string message, LogContext? context = null, Exception? ex = null);
        Task LogDebugAsync(string message, LogContext? context = null, Exception? ex = null);
        Task LogInformationAsync(string message, LogContext? context = null, Exception? ex = null);
        Task LogWarningAsync(string message, LogContext? context = null, Exception? ex = null);
        Task LogErrorAsync(string message, LogContext? context = null, Exception? ex = null);
    }

    /// <summary>
    /// Optional bag of contextual identifiers attached to a log entry.
    /// Populate only the fields that are meaningful for the operation being logged.
    /// </summary>
    public class LogContext
    {
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? VeichleId { get; set; }
        public string? VeichleLicensePlate { get; set; }
        public string? WorkflowId { get; set; }
        public string? WorkflowName { get; set; }
        public string? WorkflowExecutionId { get; set; }
        public string? WorkflowTaskId { get; set; }
        public string? WorkflowTaskName { get; set; }
        public string? AdditionalInfo { get; set; }
    }

    /// <summary>
    /// Scoped service that writes AppLog entries to MongoDB.
    /// Respects the company-wide log level configured via LogSettingsService.
    /// Never throws — logging failures are silently swallowed so they never break the main flow.
    /// </summary>
    public class AppLogger : IAppLogger
    {
        private readonly IDbDependency _db;
        private readonly LogSettingsService _logSettings;

        public AppLogger(IDbDependency db, LogSettingsService logSettings)
        {
            _db = db;
            _logSettings = logSettings;
        }

        public async Task LogAsync(AppLogLevel level, string message, LogContext? context = null, Exception? ex = null)
        {
            if (!_logSettings.ShouldLog(level))
                return;

            try
            {
                var log = new AppLog
                {
                    Id = Guid.NewGuid().ToString(),
                    Timestamp = DateTime.UtcNow,
                    Level = level.ToString(),
                    Message = message,
                    Exception = ex?.ToString(),
                    EmployeeId = context?.EmployeeId,
                    EmployeeName = context?.EmployeeName,
                    CustomerId = context?.CustomerId,
                    CustomerName = context?.CustomerName,
                    VeichleId = context?.VeichleId,
                    VeichleLicensePlate = context?.VeichleLicensePlate,
                    WorkflowId = context?.WorkflowId,
                    WorkflowName = context?.WorkflowName,
                    WorkflowExecutionId = context?.WorkflowExecutionId,
                    WorkflowTaskId = context?.WorkflowTaskId,
                    WorkflowTaskName = context?.WorkflowTaskName,
                    AdditionalInfo = context?.AdditionalInfo
                };

                await _db.GetDataAccessAppLog().InsertAsync(log);
            }
            catch
            {
                // Intentionally swallowed — logging must never break the main flow.
            }
        }

        public Task LogDebugAsync(string message, LogContext? context = null, Exception? ex = null)
            => LogAsync(AppLogLevel.Debug, message, context, ex);

        public Task LogInformationAsync(string message, LogContext? context = null, Exception? ex = null)
            => LogAsync(AppLogLevel.Information, message, context, ex);

        public Task LogWarningAsync(string message, LogContext? context = null, Exception? ex = null)
            => LogAsync(AppLogLevel.Warning, message, context, ex);

        public Task LogErrorAsync(string message, LogContext? context = null, Exception? ex = null)
            => LogAsync(AppLogLevel.Error, message, context, ex);
    }
}
