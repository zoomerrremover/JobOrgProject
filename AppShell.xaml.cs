using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
            Routing.RegisterRoute(nameof(WorkerDetailPage), typeof(WorkerDetailPage));
            Routing.RegisterRoute(nameof(ScheldudePage), typeof(ScheldudePage));
            Routing.RegisterRoute(nameof(GlobalSearchPage), typeof(GlobalSearchPage));
            InitializeComponent();
        }
    }
}