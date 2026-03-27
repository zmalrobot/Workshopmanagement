using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Singleton service that holds the current theme for the whole application.
    /// Registering as Singleton means all connected users share the same theme,
    /// which is the desired "company-wide" behaviour.
    /// </summary>
    public class ThemeService
    {
        private ThemeVariant _currentVariant = ThemeVariant.MaterialExpressiveDark;
        private readonly object _lock = new();

        public ThemeVariant CurrentVariant
        {
            get => _currentVariant;
            private set => _currentVariant = value;
        }

        /// <summary>Fired when the theme changes. Subscribers must InvokeAsync(StateHasChanged).</summary>
        public event Action? OnThemeChanged;

        /// <summary>True if the theme has been loaded from the database at least once.</summary>
        public bool IsInitialized { get; private set; }

        public void SetTheme(ThemeVariant variant)
        {
            lock (_lock)
            {
                _currentVariant = variant;
                IsInitialized = true;
            }
            OnThemeChanged?.Invoke();
        }

        public void InitializeFromDb(ThemeVariant variant)
        {
            lock (_lock)
            {
                if (IsInitialized) return;
                _currentVariant = variant;
                IsInitialized = true;
            }
            // No event fire – this is a silent init on first load
        }

        public MudTheme GetMudTheme() => _currentVariant switch
        {
            ThemeVariant.MaterialExpressiveDark  => MaterialExpressiveTheme.Dark,
            ThemeVariant.MaterialExpressiveLight => MaterialExpressiveTheme.Light,
            ThemeVariant.AppleLiquidGlassDark    => AppleLiquidGlassTheme.Dark,
            ThemeVariant.AppleLiquidGlassLight   => AppleLiquidGlassTheme.Light,
            ThemeVariant.FluentDesignDark        => FluentDesignTheme.Dark,
            ThemeVariant.FluentDesignLight       => FluentDesignTheme.Light,
            ThemeVariant.CarbonDark              => CarbonTheme.Dark,
            ThemeVariant.CarbonLight             => CarbonTheme.Light,
            _                                    => MaterialExpressiveTheme.Dark
        };

        public bool IsDarkMode => _currentVariant switch
        {
            ThemeVariant.MaterialExpressiveLight => false,
            ThemeVariant.AppleLiquidGlassLight   => false,
            ThemeVariant.FluentDesignLight       => false,
            ThemeVariant.CarbonLight             => false,
            _                                    => true
        };

        /// <summary>CSS class applied to the root wrapper div in MainLayout for extra visual effects.</summary>
        public string GetBodyClass() => _currentVariant switch
        {
            ThemeVariant.MaterialExpressiveDark  => "theme-md3-dark",
            ThemeVariant.MaterialExpressiveLight => "theme-md3-light",
            ThemeVariant.AppleLiquidGlassDark    => "theme-glass-dark",
            ThemeVariant.AppleLiquidGlassLight   => "theme-glass-light",
            ThemeVariant.FluentDesignDark        => "theme-fluent-dark",
            ThemeVariant.FluentDesignLight       => "theme-fluent-light",
            ThemeVariant.CarbonDark              => "theme-carbon-dark",
            ThemeVariant.CarbonLight             => "theme-carbon-light",
            _                                    => "theme-md3-dark"
        };
    }
}
