using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// IBM Carbon Design System — enterprise-grade, crisp, high-contrast.
    /// Zero glass effects. Sharp corners, strong focus rings, pure data-first aesthetic.
    /// Primary: IBM Blue 60 (#0F62FE), Font: IBM Plex Sans.
    /// </summary>
    public static class CarbonTheme
    {
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary               = "#0F62FE",        // IBM Blue 60
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#8A3FFC",        // IBM Purple 60
                SecondaryContrastText = "#FFFFFF",
                Background            = "#161616",        // IBM Gray 100
                Surface               = "#262626",        // IBM Gray 90
                DrawerBackground      = "#1C1C1C",        // IBM Layer 01
                AppbarBackground      = "#161616",
                DrawerText            = "#F4F4F4",
                AppbarText            = "#F4F4F4",
                TextPrimary           = "#F4F4F4",        // IBM Gray 10
                TextSecondary         = "#C6C6C6",        // IBM Gray 30
                TextDisabled          = "rgba(244,244,244,0.25)",
                Success               = "#42BE65",        // IBM Green 40
                Warning               = "#F1C21B",        // IBM Yellow 30
                Error                 = "#FF8389",        // IBM Red 40
                Info                  = "#33B1FF",        // IBM Cyan 40
                ActionDefault         = "#C6C6C6",
                ActionDisabled        = "rgba(244,244,244,0.25)",
                ActionDisabledBackground = "rgba(244,244,244,0.06)",
                LinesDefault          = "rgba(255,255,255,0.08)",
                LinesInputs           = "rgba(255,255,255,0.20)",
                TableHover            = "rgba(15,98,254,0.10)",
                Divider               = "rgba(255,255,255,0.08)",
                OverlayDark           = "rgba(0,0,0,0.60)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "2px",             // Carbon: nearly square
                DrawerWidthLeft     = "256px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily    = new[] { "'IBM Plex Sans'", "system-ui", "-apple-system", "sans-serif" },
                    FontSize      = "0.875rem",
                    FontWeight    = "400",
                    LineHeight    = "1.5",
                    LetterSpacing = "0.16px"
                },
                H1 = new H1Typography { FontWeight = "300", FontSize = "2.625rem",  LetterSpacing = "-0.01em" },
                H2 = new H2Typography { FontWeight = "300", FontSize = "2rem",       LetterSpacing = "-0.005em" },
                H3 = new H3Typography { FontWeight = "400", FontSize = "1.5rem"  },
                H4 = new H4Typography { FontWeight = "400", FontSize = "1.25rem" },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1rem"    },
                H6 = new H6Typography { FontWeight = "600", FontSize = "0.875rem" },
                Subtitle1 = new Subtitle1Typography { FontWeight = "600", LetterSpacing = "0.16px" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "600", LetterSpacing = "0.16px" },
                Body1 = new Body1Typography  { FontSize = "0.875rem", LetterSpacing = "0.16px" },
                Body2 = new Body2Typography  { FontSize = "0.75rem",  LetterSpacing = "0.32px" },
                Button = new ButtonTypography
                {
                    FontWeight    = "400",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0.16px",
                    TextTransform = "none"
                },
                Caption = new CaptionTypography { FontSize = "0.75rem", LetterSpacing = "0.32px" },
                Overline = new OverlineTypography { FontSize = "0.75rem", LetterSpacing = "0.32px", TextTransform = "uppercase" },
            }
        };

        public static MudTheme Light => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary               = "#0F62FE",        // IBM Blue 60
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#8A3FFC",        // IBM Purple 60
                SecondaryContrastText = "#FFFFFF",
                Background            = "#F4F4F4",        // IBM Gray 10
                Surface               = "#FFFFFF",        // IBM White
                DrawerBackground      = "#E0E0E0",        // IBM Gray 20
                AppbarBackground      = "#FFFFFF",
                DrawerText            = "#161616",
                AppbarText            = "#161616",
                TextPrimary           = "#161616",        // IBM Gray 100
                TextSecondary         = "#525252",        // IBM Gray 60
                TextDisabled          = "rgba(22,22,22,0.25)",
                Success               = "#24A148",        // IBM Green 50
                Warning               = "#F1C21B",        // IBM Yellow 30
                Error                 = "#DA1E28",        // IBM Red 60
                Info                  = "#0043CE",        // IBM Blue 70
                ActionDefault         = "rgba(22,22,22,0.70)",
                ActionDisabled        = "rgba(22,22,22,0.25)",
                ActionDisabledBackground = "rgba(22,22,22,0.08)",
                LinesDefault          = "#E0E0E0",
                LinesInputs           = "#8D8D8D",
                TableHover            = "rgba(15,98,254,0.06)",
                Divider               = "#E0E0E0",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "2px",
                DrawerWidthLeft     = "256px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily    = new[] { "'IBM Plex Sans'", "system-ui", "-apple-system", "sans-serif" },
                    FontSize      = "0.875rem",
                    FontWeight    = "400",
                    LineHeight    = "1.5",
                    LetterSpacing = "0.16px"
                },
                H1 = new H1Typography { FontWeight = "300", FontSize = "2.625rem",  LetterSpacing = "-0.01em" },
                H2 = new H2Typography { FontWeight = "300", FontSize = "2rem",       LetterSpacing = "-0.005em" },
                H3 = new H3Typography { FontWeight = "400", FontSize = "1.5rem"  },
                H4 = new H4Typography { FontWeight = "400", FontSize = "1.25rem" },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1rem"    },
                H6 = new H6Typography { FontWeight = "600", FontSize = "0.875rem" },
                Subtitle1 = new Subtitle1Typography { FontWeight = "600", LetterSpacing = "0.16px" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "600", LetterSpacing = "0.16px" },
                Body1 = new Body1Typography  { FontSize = "0.875rem", LetterSpacing = "0.16px" },
                Body2 = new Body2Typography  { FontSize = "0.75rem",  LetterSpacing = "0.32px" },
                Button = new ButtonTypography
                {
                    FontWeight    = "400",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0.16px",
                    TextTransform = "none"
                },
                Caption = new CaptionTypography { FontSize = "0.75rem", LetterSpacing = "0.32px" },
                Overline = new OverlineTypography { FontSize = "0.75rem", LetterSpacing = "0.32px", TextTransform = "uppercase" },
            }
        };
    }
}
