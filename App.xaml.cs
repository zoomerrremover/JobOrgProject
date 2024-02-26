using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.View.MainPages;

namespace TheJobOrganizationApp
{
    public partial class App : Application
    {
        public App(LoadingPage page)
        {
            
            InitializeComponent();

            MainPage = new AppShell();
            Shell.Current.Navigation.PushAsync(page);
        }
    }
}