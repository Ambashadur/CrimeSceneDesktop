using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace CrimeSceneDesktop;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Current.UserAppTheme = AppTheme.Light;
        MainPage = new AppShell();

        Routing.RegisterRoute("commonPage", typeof(CommonPage));
        Routing.RegisterRoute("mainPage", typeof(MainPage));
    }
}
