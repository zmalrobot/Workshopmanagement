using MudBlazor;

namespace Workshopmanagement.Utility.Themes
{
    public class VeniceCamperTheme : ICamperTheme
    {
        public MudTheme GetTheme()
        {
            return new MudTheme()
            {
                PaletteLight = new PaletteLight()
                {
                    Primary = "#1C90F3", // Blue
                    Secondary = "#71BF44", // Green
                    Info = "#1C90F3", // Matching primary for info
                    Success = "#71BF44", // Matching secondary for success
                    Warning = "#FFD700", // Yellow
                    Error = "#F44336", // Default error color, can be customized
                    Background = "#F0F0F0", // Light background color
                    Surface = "#FFFFFF", // White surface for cards and containers
                    DrawerBackground = "#1C90F3", // Blue drawer background
                    DrawerText = "rgba(255,255,255, 0.70)", // Light text for drawer
                    AppbarBackground = "#1C90F3", // Blue app bar background
                    AppbarText = "rgba(255,255,255, 0.80)" // Light text for app bar
                },
                PaletteDark = new PaletteDark()
                {
                    Primary = "#1C90F3", // Blue
                    Secondary = "#71BF44", // Green
                    Info = "#1C90F3", // Matching primary for info
                    Success = "#71BF44", // Matching secondary for success
                    Warning = "#FFD700", // Yellow
                    Error = "#F44336", // Red for error messages
                    Background = "#1E1E2D", // Dark background color
                    Surface = "#2E2E3E", // Slightly lighter dark color for surfaces
                    DrawerBackground = "#2A2A3B", // Darker drawer background
                    DrawerText = "rgba(255,255,255, 0.70)", // Light text for drawer
                    AppbarBackground = "#1C90F3", // Keep app bar background blue for contrast
                    AppbarText = "rgba(255,255,255, 0.80)", // Light text for app bar
                    TextPrimary = "rgba(255,255,255, 0.87)", // Primary text color
                    TextSecondary = "rgba(255,255,255, 0.60)", // Secondary text color
                    ActionDefault = "#D0D0D0", // Default color for icons and actions
                    ActionDisabled = "rgba(255,255,255, 0.38)", // Disabled color for contrast
                    ActionDisabledBackground = "rgba(255,255,255, 0.12)", // Background for disabled items
                },
                LayoutProperties = new LayoutProperties()
                {
                    DefaultBorderRadius = "6px",
                },
                Typography = new Typography()
                {                  
                    Default = new DefaultTypography
                    {
                        FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                    },
                    H1 = new H1Typography
                    {
                        FontSize = "2.5rem",
                        FontWeight = "600",
                    },
                    Button = new ButtonTypography
                    {
                        FontWeight = "600",
                    }
                }
            };

        }
    }
}
