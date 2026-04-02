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
        private ThemeVariant _currentVariant = ThemeVariant.ChromaDark;
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
            ThemeVariant.ChromaDark         => ChromaTheme.Dark,
            ThemeVariant.ChromaLight        => ChromaTheme.Light,
            ThemeVariant.FrostedGlassDark   => FrostedGlassTheme.Dark,
            ThemeVariant.FrostedGlassLight  => FrostedGlassTheme.Light,
            ThemeVariant.AcrylicDark        => AcrylicTheme.Dark,
            ThemeVariant.AcrylicLight       => AcrylicTheme.Light,
            ThemeVariant.ForgeDark          => ForgeTheme.Dark,
            ThemeVariant.ForgeLight         => ForgeTheme.Light,
            ThemeVariant.RetroWebDark       => RetroWebTheme.Dark,
            ThemeVariant.RetroWebLight      => RetroWebTheme.Light,
            _                               => ChromaTheme.Dark
        };

        public bool IsDarkMode => _currentVariant switch
        {
            ThemeVariant.ChromaLight        => false,
            ThemeVariant.FrostedGlassLight  => false,
            ThemeVariant.AcrylicLight       => false,
            ThemeVariant.ForgeLight         => false,
            ThemeVariant.RetroWebLight      => false,
            _                               => true
        };

        /// <summary>CSS class applied to the root wrapper div in MainLayout for extra visual effects.</summary>
        public string GetBodyClass() => _currentVariant switch
        {
            ThemeVariant.ChromaDark         => "theme-md3-dark",
            ThemeVariant.ChromaLight        => "theme-md3-light",
            ThemeVariant.FrostedGlassDark   => "theme-glass-dark",
            ThemeVariant.FrostedGlassLight  => "theme-glass-light",
            ThemeVariant.AcrylicDark        => "theme-fluent-dark",
            ThemeVariant.AcrylicLight       => "theme-fluent-light",
            ThemeVariant.ForgeDark          => "theme-carbon-dark",
            ThemeVariant.ForgeLight         => "theme-carbon-light",
            ThemeVariant.RetroWebDark       => "theme-retro-dark",
            ThemeVariant.RetroWebLight      => "theme-retro-light",
            _                               => "theme-md3-dark"
        };
    }
}
