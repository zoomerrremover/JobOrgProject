using TheJobOrganizationApp.View;
using TheJobOrganizationApp.View.MainPages;

namespace TheJobOrganizationApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
            Routing.RegisterRoute(nameof(LogInPage2), typeof(LogInPage2));
            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(ScheldudePage), typeof(ScheldudePage));
            Routing.RegisterRoute(nameof(GlobalSearchPage), typeof(GlobalSearchPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            InitializeComponent();
        }
    }
}