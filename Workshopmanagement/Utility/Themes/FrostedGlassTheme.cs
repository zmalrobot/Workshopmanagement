using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Frosted Glass — translucent frosted-glass surfaces, vibrancy,
    /// system blue/green accents, tight letter-spacing, clean typography.
    /// </summary>
    public static class FrostedGlassTheme
    {
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary               = "#0A84FF",        // iOS Blue dark
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#FF375F",        // iOS Pink dark
                SecondaryContrastText = "#FFFFFF",
                Tertiary              = "#30D158",        // iOS Green dark
                Background            = "#000000",        // True OLED black
                Surface               = "#1C1C1E",        // iOS grouped background
                DrawerBackground      = "#2C2C2E",        // iOS elevated background
                AppbarBackground      = "rgba(28,28,30,0.82)",
                DrawerText            = "rgba(255,255,255,0.90)",
                AppbarText            = "rgba(255,255,255,0.90)",
                TextPrimary           = "rgba(255,255,255,0.90)",
                TextSecondary         = "rgba(235,235,245,0.55)",
                Success               = "#30D158",
                Warning               = "#FF9F0A",
                Error                 = "#FF453A",
                Info                  = "#64D2FF",        // iOS Cyan
                ActionDefault         = "rgba(255,255,255,0.70)",
                ActionDisabled        = "rgba(255,255,255,0.28)",
                ActionDisabledBackground = "rgba(255,255,255,0.08)",
                LinesDefault          = "rgba(255,255,255,0.10)",
                LinesInputs           = "rgba(255,255,255,0.20)",
                TableHover            = "rgba(10,132,255,0.08)",
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
                Primary               = "#007AFF",        // iOS Blue
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#FF2D55",        // iOS Pink
                SecondaryContrastText = "#FFFFFF",
                Tertiary              = "#34C759",        // iOS Green
                Background            = "#F2F2F7",        // iOS systemGroupedBackground
                Surface               = "#FFFFFF",
                DrawerBackground      = "rgba(242,242,247,0.90)",
                AppbarBackground      = "rgba(242,242,247,0.82)",
                DrawerText            = "rgba(0,0,0,0.87)",
                AppbarText            = "rgba(0,0,0,0.87)",
                TextPrimary           = "rgba(0,0,0,0.87)",
                TextSecondary         = "rgba(60,60,67,0.55)",
                Success               = "#34C759",
                Warning               = "#FF9500",
                Error                 = "#FF3B30",
                Info                  = "#32ADE6",        // iOS Cyan/Teal
                LinesDefault          = "rgba(60,60,67,0.10)",
                LinesInputs           = "rgba(60,60,67,0.20)",
                TableHover            = "rgba(0,122,255,0.06)",
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
