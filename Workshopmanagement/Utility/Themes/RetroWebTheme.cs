using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    /// <summary>
    /// Early 2000s web aesthetic — two variants:
    /// Dark: phosphor-green terminal / "Matrix" — monospace, 0 radius, glowing green on black.
    /// Light: Windows XP / IE6 era — Verdana, silver-gray chrome, classic blue &amp; orange.
    /// </summary>
    public static class RetroWebTheme
    {
        // ── DARK — phosphor terminal / cyber ────────────────────────────────
        public static MudTheme Dark => new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Primary               = "#00FF41",        // Matrix phosphor green
                PrimaryContrastText   = "#000000",
                Secondary             = "#00FFFF",        // CRT cyan — classic terminal
                SecondaryContrastText = "#000000",
                Tertiary              = "#FFFF00",        // Terminal amber/yellow
                Background            = "#000000",        // CRT black
                Surface               = "#080808",        // Barely-elevated surface
                DrawerBackground      = "#030303",
                AppbarBackground      = "#000000",
                DrawerText            = "#00FF41",
                AppbarText            = "#00FF41",
                TextPrimary           = "#00FF41",
                TextSecondary         = "#00B32D",        // Dimmer phosphor
                TextDisabled          = "rgba(0,255,65,0.28)",
                Success               = "#00FF41",        // Phosphor green = success
                Warning               = "#FFFF00",        // Yellow = warning
                Error                 = "#FF0000",        // Pure red = error
                Info                  = "#00FFFF",        // Cyan = info
                ActionDefault         = "#00FF41",
                ActionDisabled        = "rgba(0,255,65,0.28)",
                ActionDisabledBackground = "rgba(0,255,65,0.04)",
                LinesDefault          = "rgba(0,255,65,0.18)",
                LinesInputs           = "rgba(0,255,65,0.38)",
                TableHover            = "rgba(0,255,65,0.05)",
                Divider               = "rgba(0,255,65,0.18)",
                OverlayDark           = "rgba(0,0,0,0.90)",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "0px",             // Maximum squareness
                DrawerWidthLeft     = "240px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily    = new[] { "'Courier New'", "Courier", "monospace" },
                    FontSize      = "0.875rem",
                    FontWeight    = "400",
                    LineHeight    = "1.6",
                    LetterSpacing = "0.04em"
                },
                H1 = new H1Typography { FontWeight = "700", FontSize = "2rem",    LetterSpacing = "0.05em" },
                H2 = new H2Typography { FontWeight = "700", FontSize = "1.625rem", LetterSpacing = "0.04em" },
                H3 = new H3Typography { FontWeight = "700", FontSize = "1.375rem" },
                H4 = new H4Typography { FontWeight = "700", FontSize = "1.125rem" },
                H5 = new H5Typography { FontWeight = "700", FontSize = "1rem"     },
                H6 = new H6Typography { FontWeight = "700", FontSize = "0.875rem" },
                Subtitle1 = new Subtitle1Typography { FontWeight = "700", LetterSpacing = "0.04em" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "700", LetterSpacing = "0.04em" },
                Body1 = new Body1Typography  { FontSize = "0.875rem", LetterSpacing = "0.03em" },
                Body2 = new Body2Typography  { FontSize = "0.8125rem" },
                Button = new ButtonTypography
                {
                    FontFamily    = new[] { "'Courier New'", "Courier", "monospace" },
                    FontWeight    = "700",
                    FontSize      = "0.875rem",
                    LetterSpacing = "0.08em",
                    TextTransform = "uppercase"
                },
                Caption = new CaptionTypography  { FontSize = "0.75rem", LetterSpacing = "0.04em" },
                Overline = new OverlineTypography { FontSize = "0.75rem", LetterSpacing = "0.10em", TextTransform = "uppercase" },
            }
        };

        // ── LIGHT — Windows XP / IE6 era ────────────────────────────────────
        public static MudTheme Light => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary               = "#003399",        // Classic IE/Netscape nav blue
                PrimaryContrastText   = "#FFFFFF",
                Secondary             = "#006600",        // Classic web green
                SecondaryContrastText = "#FFFFFF",
                Tertiary              = "#990000",        // Classic web dark red
                Background            = "#ECE9D8",        // Windows XP Luna silver-tan
                Surface               = "#FFFFFF",
                DrawerBackground      = "#C7C5B5",        // Darker XP chrome
                AppbarBackground      = "#003399",        // IE6 toolbar blue
                DrawerText            = "#000000",
                AppbarText            = "#FFFFFF",
                TextPrimary           = "#000000",
                TextSecondary         = "#333333",
                TextDisabled          = "#999999",
                Success               = "#006600",
                Warning               = "#FF6600",        // Classic web orange
                Error                 = "#CC0000",        // Classic web red
                Info                  = "#003399",
                ActionDefault         = "rgba(0,0,0,0.70)",
                ActionDisabled        = "rgba(0,0,0,0.26)",
                ActionDisabledBackground = "rgba(0,0,0,0.08)",
                LinesDefault          = "#ACA899",        // XP border
                LinesInputs           = "#7F9DB9",        // XP input border
                TableHover            = "rgba(0,51,153,0.05)",
                Divider               = "#ACA899",
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "0px",
                DrawerWidthLeft     = "240px",
            },
            Typography = new Typography
            {
                Default = new DefaultTypography
                {
                    FontFamily    = new[] { "Verdana", "Arial", "Helvetica", "sans-serif" },
                    FontSize      = "0.8125rem",          // 13px — classic IE default
                    FontWeight    = "400",
                    LineHeight    = "1.5",
                    LetterSpacing = "0"
                },
                H1 = new H1Typography { FontWeight = "700", FontSize = "1.875rem" },
                H2 = new H2Typography { FontWeight = "700", FontSize = "1.5rem"   },
                H3 = new H3Typography { FontWeight = "700", FontSize = "1.25rem"  },
                H4 = new H4Typography { FontWeight = "700", FontSize = "1.125rem" },
                H5 = new H5Typography { FontWeight = "700", FontSize = "1rem"     },
                H6 = new H6Typography { FontWeight = "700", FontSize = "0.875rem" },
                Subtitle1 = new Subtitle1Typography { FontWeight = "700" },
                Subtitle2 = new Subtitle2Typography { FontWeight = "700" },
                Body1 = new Body1Typography  { FontSize = "0.8125rem" },
                Body2 = new Body2Typography  { FontSize = "0.75rem"   },
                Button = new ButtonTypography
                {
                    FontFamily    = new[] { "Verdana", "Arial", "sans-serif" },
                    FontWeight    = "700",
                    FontSize      = "0.8125rem",
                    LetterSpacing = "0",
                    TextTransform = "none"
                },
                Caption = new CaptionTypography  { FontSize = "0.6875rem" },
                Overline = new OverlineTypography { FontSize = "0.6875rem", TextTransform = "uppercase" },
            }
        };
    }
}
