using MudBlazor;
public class MudBlazorAdminDashboard : MudTheme
{
    public MudBlazorAdminDashboard()
    {
        Palette.Primary = "#2082ce";
        PaletteDark.Primary = "#2082ce";
        Palette.Background = "#bababa";
        Palette.AppbarBackground = "#FFF";
        Palette.Divider = "#0000001e";
        Palette.AppbarText = "#000";
        Shadows.Elevation[1] = "0px 3px 5px -1px rgba(0,0,0,0.2),0px 5px 8px 0px rgba(0,0,0,0.14),0px 1px 14px 0px rgba(0,0,0,0.12)";
        LayoutProperties.DefaultBorderRadius = "6px";
    }
}

