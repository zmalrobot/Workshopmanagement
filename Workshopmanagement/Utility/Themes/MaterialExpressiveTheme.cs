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
                Primary             = "#D0BCFF",          // Lavender 80
                PrimaryContrastText = "#381E72",
                Secondary           = "#CCC2DC",           // Secondary 80
                SecondaryContrastText = "#332D41",
                Tertiary            = "#EFB8C8",           // Tertiary 80
                Background          = "#0F0D13",           // Neutral 6
                Surface             = "#1C1B1F",           // Neutral 12
                DrawerBackground    = "#16142A",
                AppbarBackground    = "#1C1B1F",
                DrawerText          = "rgba(255,255,255, 0.87)",
                AppbarText          = "rgba(255,255,255, 0.87)",
                TextPrimary         = "rgba(255,255,255, 0.87)",
                TextSecondary       = "rgba(255,255,255, 0.60)",
                Success             = "#78DC77",           // Tonal green
                Warning             = "#FFB951",           // Tonal amber
                Error               = "#FFB4AB",           // Error 80
                Info                = "#97CBFF",           // Tonal blue
                ActionDefault       = "rgba(202,196,208,0.87)",
                ActionDisabled      = "rgba(202,196,208,0.38)",
                ActionDisabledBackground = "rgba(202,196,208,0.12)",
                LinesDefault        = "rgba(202,196,208,0.12)",
                LinesInputs         = "rgba(202,196,208,0.23)",
                TableHover          = "rgba(208,188,255,0.08)",
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
                    FontFamily   = new[] { "Google Sans", "Roboto", "Helvetica", "sans-serif" },
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
                Primary             = "#6750A4",          // Primary 40
                PrimaryContrastText = "#FFFFFF",
                Secondary           = "#625B71",           // Secondary 40
                Tertiary            = "#7D5260",           // Tertiary 40
                Background          = "#FFFBFE",           // Neutral 99
                Surface             = "#FEF7FF",           // Neutral-variant 99
                DrawerBackground    = "#ECE6F0",           // Surface-variant
                AppbarBackground    = "#6750A4",
                DrawerText          = "rgba(28,27,31, 0.87)",
                AppbarText          = "#FFFFFF",
                TextPrimary         = "rgba(28,27,31, 0.87)",
                TextSecondary       = "rgba(28,27,31, 0.60)",
                Success             = "#386A20",
                Warning             = "#7D5700",
                Error               = "#B3261E",
                Info                = "#005CBB",
                LinesDefault        = "rgba(121,116,126,0.12)",
                LinesInputs         = "rgba(121,116,126,0.23)",
                TableHover          = "rgba(103,80,164,0.06)",
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
                    FontFamily   = new[] { "Google Sans", "Roboto", "Helvetica", "sans-serif" },
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
