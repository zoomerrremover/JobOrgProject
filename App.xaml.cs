using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.View.MainPages;

namespace TheJobOrganizationApp
{
    public partial class App : Application
    {
        public App()
        {
            
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}