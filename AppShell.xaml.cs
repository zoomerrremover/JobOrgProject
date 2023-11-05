using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            InitializeComponent();
        }
    }
}