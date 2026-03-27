using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Apple Liquid Glass — translucent frosted-glass surfaces, iOS/macOS vibrancy,
    /// system blue/green accents, tight letter-spacing, SF Pro-inspired typography.
    /// </summary>
    public static class AppleLiquidGlassTheme
    {
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary             = "#0A84FF",          // iOS Blue (dark)
                PrimaryContrastText = "#FFFFFF",
                Secondary           = "#30D158",           // iOS Green (dark)
                SecondaryContrastText = "#FFFFFF",
                Tertiary            = "#FF9F0A",           // iOS Orange (dark)
                Background          = "#000000",           // OLED black
                Surface             = "#1C1C1E",           // iOS grouped background level 2
                DrawerBackground    = "#1C1C1E",
                AppbarBackground    = "rgba(28,28,30,0.78)",
                DrawerText          = "rgba(255,255,255, 0.87)",
                AppbarText          = "rgba(255,255,255, 0.87)",
                TextPrimary         = "rgba(255,255,255, 0.87)",
                TextSecondary       = "rgba(255,255,255, 0.55)",
                Success             = "#30D158",
                Warning             = "#FF9F0A",
                Error               = "#FF453A",
                Info                = "#0A84FF",
                ActionDefault       = "rgba(255,255,255,0.70)",
                ActionDisabled      = "rgba(255,255,255,0.28)",
                ActionDisabledBackground = "rgba(255,255,255,0.08)",
                LinesDefault        = "rgba(255,255,255,0.10)",
                LinesInputs         = "rgba(255,255,255,0.18)",
                TableHover          = "rgba(10,132,255,0.08)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "14px",
                DrawerWidthLeft     = "260px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily   = new[] { "-apple-system", "BlinkMacSystemFont", "SF Pro Text", "Helvetica Neue", "sans-serif" },
                    FontSize     = "0.9375rem",
                    FontWeight   = "400",
                    LineHeight   = "1.47",
                    LetterSpacing = "-0.01em"
                },
                H1 = new H1Typography { FontWeight = "700", FontSize = "2.5rem",  LetterSpacing = "-0.03em" },
                H2 = new H2Typography { FontWeight = "700", FontSize = "2rem",    LetterSpacing = "-0.025em" },
                H3 = new H3Typography { FontWeight = "600", FontSize = "1.75rem", LetterSpacing = "-0.02em" },
                H4 = new H4Typography { FontWeight = "600", FontSize = "1.5rem",  LetterSpacing = "-0.015em" },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1.25rem", LetterSpacing = "-0.01em" },
                H6 = new H6Typography { FontWeight = "600", FontSize = "1.0625rem", LetterSpacing = "-0.005em" },
                Subtitle1 = new Subtitle1Typography { FontWeight = "600", LetterSpacing = "-0.01em" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "600", LetterSpacing = "-0.005em" },
                Body1 = new Body1Typography { LetterSpacing = "-0.005em" },
                Button = new ButtonTypography
                {
                    FontWeight    = "590",
                    FontSize      = "0.9375rem",
                    LetterSpacing = "-0.01em",
                    TextTransform = "none"
                },
                Caption = new CaptionTypography { FontSize = "0.75rem", LetterSpacing = "0" },
            }
        };

        public static MudTheme Light => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary             = "#007AFF",          // iOS Blue (light)
                PrimaryContrastText = "#FFFFFF",
                Secondary           = "#34C759",           // iOS Green (light)
                SecondaryContrastText = "#FFFFFF",
                Tertiary            = "#FF9500",           // iOS Orange (light)
                Background          = "#F2F2F7",           // iOS primary background
                Surface             = "#FFFFFF",
                DrawerBackground    = "rgba(242,242,247,0.82)",
                AppbarBackground    = "rgba(242,242,247,0.78)",
                DrawerText          = "rgba(0,0,0, 0.87)",
                AppbarText          = "rgba(0,0,0, 0.87)",
                TextPrimary         = "rgba(0,0,0, 0.87)",
                TextSecondary       = "rgba(0,0,0, 0.50)",
                Success             = "#34C759",
                Warning             = "#FF9500",
                Error               = "#FF3B30",
                Info                = "#007AFF",
                LinesDefault        = "rgba(60,60,67,0.10)",
                LinesInputs         = "rgba(60,60,67,0.18)",
                TableHover          = "rgba(0,122,255,0.06)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "14px",
                DrawerWidthLeft     = "260px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily   = new[] { "-apple-system", "BlinkMacSystemFont", "SF Pro Text", "Helvetica Neue", "sans-serif" },
                    FontSize     = "0.9375rem",
                    FontWeight   = "400",
                    LineHeight   = "1.47",
                    LetterSpacing = "-0.01em"
                },
                H1 = new H1Typography { FontWeight = "700", FontSize = "2.5rem",  LetterSpacing = "-0.03em" },
                H2 = new H2Typography { FontWeight = "700", FontSize = "2rem",    LetterSpacing = "-0.025em" },
                H3 = new H3Typography { FontWeight = "600", FontSize = "1.75rem", LetterSpacing = "-0.02em" },
                H4 = new H4Typography { FontWeight = "600", FontSize = "1.5rem",  LetterSpacing = "-0.015em" },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1.25rem", LetterSpacing = "-0.01em" },
                H6 = new H6Typography { FontWeight = "600", FontSize = "1.0625rem" },
                Button = new ButtonTypography
                {
                    FontWeight    = "590",
                    FontSize      = "0.9375rem",
                    LetterSpacing = "-0.01em",
                    TextTransform = "none"
                },
            }
        };
    }
}
