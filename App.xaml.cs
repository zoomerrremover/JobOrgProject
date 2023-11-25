using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;
namespace TheJobOrganizationApp
{
    public partial class App : Application
    {
        public App(/*Initializator init,IDataStorage dataBase,GlobalSettings gs*/)
        {
            
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}