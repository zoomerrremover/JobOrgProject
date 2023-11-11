using TheJobOrganizationApp.View;
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