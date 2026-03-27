using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Material Design 3 Expressive — rich tonal palette, very rounded corners,
    /// Google Sans typography, dynamic colour tinting.
    /// </summary>
    public static class MaterialExpressiveTheme
    {
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary               = "#CE8FFF",        // Expressive vivid violet
                PrimaryContrastText   = "#1A0040",
                Secondary             = "#FFB347",        // Expressive amber
                SecondaryContrastText = "#2A1500",
                Tertiary              = "#5CF5D0",        // Expressive mint
                Background            = "#0D0823",        // Deep indigo void
                Surface               = "#1E1236",        // Elevated indigo surface
                DrawerBackground      = "#160C2E",
                AppbarBackground      = "#1E1236",
                DrawerText            = "rgba(255,255,255,0.92)",
                AppbarText            = "rgba(255,255,255,0.92)",
                TextPrimary           = "rgba(255,255,255,0.92)",
                TextSecondary         = "rgba(255,255,255,0.60)",
                Success               = "#50FA7B",        // Vivid green
                Warning               = "#FFD740",        // Vivid amber
                Error                 = "#FF5370",        // Vivid red
                Info                  = "#82AAFF",        // Vivid blue
                ActionDefault         = "rgba(206,143,255,0.87)",
                ActionDisabled        = "rgba(206,143,255,0.38)",
                ActionDisabledBackground = "rgba(206,143,255,0.12)",
                LinesDefault          = "rgba(206,143,255,0.12)",
                LinesInputs           = "rgba(206,143,255,0.23)",
                TableHover            = "rgba(206,143,255,0.08)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "16px",
                DrawerWidthLeft     = "260px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily   = new[] { "Inter", "Roboto", "Helvetica", "sans-serif" },
                    FontSize     = "0.875rem",
                    FontWeight   = "400",
                    LineHeight   = "1.5",
                    LetterSpacing = "0.01562em"
                },
                H1 = new H1Typography { FontWeight = "700", FontSize = "2.5rem",  LetterSpacing = "-0.02em" },
                H2 = new H2Typography { FontWeight = "700", FontSize = "2rem",    LetterSpacing = "-0.01em" },
                H3 = new H3Typography { FontWeight = "600", FontSize = "1.75rem" },
                H4 = new H4Typography { FontWeight = "600", FontSize = "1.5rem"  },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1.25rem" },
                H6 = new H6Typography { FontWeight = "600", FontSize = "1rem"    },
                Subtitle1 = new Subtitle1Typography { FontWeight = "500", LetterSpacing = "0.01em" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "500", LetterSpacing = "0.01em" },
                Body1 = new Body1Typography { FontSize = "0.875rem", LetterSpacing = "0.01em" },
                Body2 = new Body2Typography { FontSize = "0.8125rem" },
                Button = new ButtonTypography
                {
                    FontWeight    = "600",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0.00714em",
                    TextTransform = "none"
                },
                Caption = new CaptionTypography { FontSize = "0.75rem", LetterSpacing = "0.03em" },
            }
        };

        public static MudTheme Light => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary               = "#7209B7",        // Bold expressive violet
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#C75000",        // Warm amber-orange
                SecondaryContrastText = "#FFFFFF",
                Tertiary              = "#006C60",        // Deep teal complement
                Background            = "#FFFBF5",        // Warm expressive cream
                Surface               = "#FFF5FF",        // Slight pink bloom
                DrawerBackground      = "#F3E8FF",        // Light lavender surface
                AppbarBackground      = "#7209B7",
                DrawerText            = "rgba(19,0,42,0.87)",
                AppbarText            = "#FFFFFF",
                TextPrimary           = "rgba(19,0,42,0.87)",
                TextSecondary         = "rgba(19,0,42,0.60)",
                Success               = "#1B6E3D",
                Warning               = "#8B5000",
                Error                 = "#C0002A",
                Info                  = "#0058B7",
                LinesDefault          = "rgba(114,9,183,0.12)",
                LinesInputs           = "rgba(114,9,183,0.23)",
                TableHover            = "rgba(114,9,183,0.06)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "16px",
                DrawerWidthLeft     = "260px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily   = new[] { "Inter", "Roboto", "Helvetica", "sans-serif" },
                    FontSize     = "0.875rem",
                    FontWeight   = "400",
                    LineHeight   = "1.5",
                    LetterSpacing = "0.01562em"
                },
                H1 = new H1Typography { FontWeight = "700", FontSize = "2.5rem",  LetterSpacing = "-0.02em" },
                H2 = new H2Typography { FontWeight = "700", FontSize = "2rem",    LetterSpacing = "-0.01em" },
                H3 = new H3Typography { FontWeight = "600", FontSize = "1.75rem" },
                H4 = new H4Typography { FontWeight = "600", FontSize = "1.5rem"  },
                H5 = new H5Typography { FontWeight = "600", FontSize = "1.25rem" },
                H6 = new H6Typography { FontWeight = "600", FontSize = "1rem"    },
                Button = new ButtonTypography
                {
                    FontWeight    = "600",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0.00714em",
                    TextTransform = "none"
                },
            }
        };
    }
}
