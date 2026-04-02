using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Acrylic — layered acrylic/mica material,
    /// cool accent blue, system UI variable fonts, subtle shadows, slightly rounded corners.
    /// </summary>
    public static class AcrylicTheme
    {
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary               = "#4CC2FF",        // Windows 11 precise accent in dark
                PrimaryContrastText   = "#002040",
                Secondary             = "#9C7EF0",        // Fluent/Copilot purple
                SecondaryContrastText = "#FFFFFF",
                Tertiary              = "#6BD5E1",        // Fluent teal
                Background            = "#1C1C1C",        // Win11 Mica base
                Surface               = "#282828",        // Win11 Layer
                DrawerBackground      = "#222222",
                AppbarBackground      = "#1C1C1C",
                DrawerText            = "rgba(255,255,255,0.87)",
                AppbarText            = "rgba(255,255,255,0.87)",
                TextPrimary           = "rgba(255,255,255,0.87)",
                TextSecondary         = "rgba(255,255,255,0.60)",
                Success               = "#6CCB5F",
                Warning               = "#FCE100",
                Error                 = "#FF99A4",
                Info                  = "#4CC2FF",
                ActionDefault         = "rgba(255,255,255,0.70)",
                ActionDisabled        = "rgba(255,255,255,0.28)",
                ActionDisabledBackground = "rgba(255,255,255,0.06)",
                LinesDefault          = "rgba(255,255,255,0.08)",
                LinesInputs           = "rgba(255,255,255,0.15)",
                TableHover            = "rgba(76,194,255,0.06)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "6px",
                DrawerWidthLeft     = "260px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily   = new[] { "Segoe UI Variable", "Segoe UI", "system-ui", "-apple-system", "sans-serif" },
                    FontSize     = "0.875rem",
                    FontWeight   = "400",
                    LineHeight   = "1.43",
                    LetterSpacing = "0"
                },
                H1 = new H1Typography { FontWeight = "600", FontSize = "2.25rem", LetterSpacing = "-0.01em" },
                H2 = new H2Typography { FontWeight = "600", FontSize = "1.875rem" },
                H3 = new H3Typography { FontWeight = "600", FontSize = "1.5rem"  },
                H4 = new H4Typography { FontWeight = "600", FontSize = "1.25rem" },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1.125rem" },
                H6 = new H6Typography { FontWeight = "600", FontSize = "1rem"    },
                Subtitle1 = new Subtitle1Typography { FontWeight = "600" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "600" },
                Body1 = new Body1Typography { FontSize = "0.875rem" },
                Body2 = new Body2Typography { FontSize = "0.8125rem" },
                Button = new ButtonTypography
                {
                    FontWeight    = "600",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0",
                    TextTransform = "none"
                },
                Caption = new CaptionTypography { FontSize = "0.75rem" },
            }
        };

        public static MudTheme Light => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary               = "#0067C0",        // Windows 11 system accent blue
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#5B5FC7",        // Fluent Teams/Copilot purple-blue
                SecondaryContrastText = "#FFFFFF",
                Tertiary              = "#038387",        // Fluent teal
                Background            = "#F3F3F3",        // Windows 11 background
                Surface               = "#FFFFFF",
                DrawerBackground      = "#E9E9E9",
                AppbarBackground      = "#FFFFFF",
                DrawerText            = "rgba(0,0,0,0.87)",
                AppbarText            = "rgba(0,0,0,0.87)",
                TextPrimary           = "rgba(0,0,0,0.87)",
                TextSecondary         = "rgba(0,0,0,0.60)",
                Success               = "#107C10",
                Warning               = "#D83B01",
                Error                 = "#A4262C",
                Info                  = "#0067C0",
                LinesDefault          = "rgba(0,0,0,0.08)",
                LinesInputs           = "rgba(0,0,0,0.15)",
                TableHover            = "rgba(0,103,192,0.05)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "6px",
                DrawerWidthLeft     = "260px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily   = new[] { "Segoe UI Variable", "Segoe UI", "system-ui", "-apple-system", "sans-serif" },
                    FontSize     = "0.875rem",
                    FontWeight   = "400",
                    LineHeight   = "1.43",
                    LetterSpacing = "0"
                },
                H1 = new H1Typography { FontWeight = "600", FontSize = "2.25rem", LetterSpacing = "-0.01em" },
                H2 = new H2Typography { FontWeight = "600", FontSize = "1.875rem" },
                H3 = new H3Typography { FontWeight = "600", FontSize = "1.5rem"  },
                H4 = new H4Typography { FontWeight = "600", FontSize = "1.25rem" },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1.125rem" },
                H6 = new H6Typography { FontWeight = "600", FontSize = "1rem"    },
                Button = new ButtonTypography
                {
                    FontWeight    = "600",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0",
                    TextTransform = "none"
                },
            }
        };
    }
}
