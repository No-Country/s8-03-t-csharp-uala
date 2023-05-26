using MudBlazor;

namespace UalaSelecionado8.Theme;

public class MudBlazorAdminDashboard : MudTheme
{
    public MudBlazorAdminDashboard()
    {
        Palette.Primary = "#FF6766";
        Palette.Secondary = "#3E6BFD";
        Palette.Background = "#FFFFFF";
        Palette.AppbarBackground = "#FFFFFF";
        Palette.Success = "#5FE600";
        Palette.Divider = "#0000001e";
        Palette.TextPrimary = "#3a3a3a";
        Palette.AppbarText = "#3a3a3a";
        Shadows.Elevation[1] = "0px 3px 5px -1px rgba(0,0,0,0.2),0px 5px 8px 0px rgba(0,0,0,0.14),0px 1px 14px 0px rgba(0,0,0,0.12)";
        LayoutProperties.DefaultBorderRadius = "6px";
    }
}