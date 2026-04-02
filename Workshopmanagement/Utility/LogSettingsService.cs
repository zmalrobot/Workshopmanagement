using Workshopmanagement.DatabaseManagment.DataObject;

namespace Workshopmanagement.Utility
{
    /// <summary>
    /// Singleton that caches the current minimum log level loaded from CompanySettings.
    /// All connected users share the same level (company-wide setting).
    /// </summary>
    public class LogSettingsService
    {
        private AppLogLevel _currentLevel = AppLogLevel.Information;
        private readonly object _lock = new();

        public AppLogLevel CurrentLevel
        {
            get { lock (_lock) { return _currentLevel; } }
        }

        public bool IsInitialized { get; private set; }

        public void SetLogLevel(string level)
        {
            if (Enum.TryParse<AppLogLevel>(level, ignoreCase: true, out var parsed))
            {
                lock (_lock)
                {
                    _currentLevel = parsed;
                    IsInitialized = true;
                }
            }
        }

        public bool ShouldLog(AppLogLevel level)
        {
            return (int)level >= (int)CurrentLevel;
        }
    }
}
