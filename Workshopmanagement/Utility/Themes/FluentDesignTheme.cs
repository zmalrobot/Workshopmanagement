using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Microsoft Fluent Design System — Windows 11 Mica/Acrylic material,
    /// Windows accent blue, Segoe UI Variable, subtle shadows, slightly rounded corners.
    /// </summary>
    public static class FluentDesignTheme
    {
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary             = "#60CDFF",          // Windows 11 accent (light variant for dark mode)
                PrimaryContrastText = "#00242D",
                Secondary           = "#74B4C8",
                SecondaryContrastText = "#FFFFFF",
                Background          = "#202020",           // Windows 11 Base
                Surface             = "#2C2C2C",           // Windows 11 Layer
                DrawerBackground    = "#252525",           // Subtle overlay
                AppbarBackground    = "#1F1F1F",
                DrawerText          = "rgba(255,255,255, 0.87)",
                AppbarText          = "rgba(255,255,255, 0.87)",
                TextPrimary         = "rgba(255,255,255, 0.87)",
                TextSecondary       = "rgba(255,255,255, 0.60)",
                Success             = "#6CCB5F",
                Warning             = "#FCE100",
                Error               = "#FF99A4",
                Info                = "#60CDFF",
                ActionDefault       = "rgba(255,255,255,0.70)",
                ActionDisabled      = "rgba(255,255,255,0.28)",
                ActionDisabledBackground = "rgba(255,255,255,0.06)",
                LinesDefault        = "rgba(255,255,255,0.08)",
                LinesInputs         = "rgba(255,255,255,0.15)",
                TableHover          = "rgba(96,205,255,0.06)",
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
                Primary             = "#0078D4",          // Microsoft Blue
                PrimaryContrastText = "#FFFFFF",
                Secondary           = "#0099BC",
                SecondaryContrastText = "#FFFFFF",
                Background          = "#F3F3F3",           // Windows 11 background
                Surface             = "#FFFFFF",
                DrawerBackground    = "#EEEEEE",           // Subtle layer
                AppbarBackground    = "#F3F3F3",
                DrawerText          = "rgba(0,0,0, 0.87)",
                AppbarText          = "rgba(0,0,0, 0.87)",
                TextPrimary         = "rgba(0,0,0, 0.87)",
                TextSecondary       = "rgba(0,0,0, 0.60)",
                Success             = "#107C10",
                Warning             = "#D83B01",
                Error               = "#A4262C",
                Info                = "#0078D4",
                LinesDefault        = "rgba(0,0,0,0.08)",
                LinesInputs         = "rgba(0,0,0,0.15)",
                TableHover          = "rgba(0,120,212,0.05)",
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
