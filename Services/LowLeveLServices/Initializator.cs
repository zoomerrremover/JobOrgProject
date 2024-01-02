
using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.PreBaked.PopupPages.Login;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.Services.LowLeveLServices
{
    public partial class Initializator : ObservableObject,IInitializator
    {
        LoginViewModel loginViewModel;
        LogInPage logInPage;
        IConnectionService connectionService;
        IErrorService errorService;
        public Initializator(IErrorService errorService,IConnectionService ConnectionService,LogInPage logInPage)
        {
            this.errorService = errorService;
            connectionService = ConnectionService;
            loginViewModel = logInPage.BindingContext as LoginViewModel;
            this.logInPage = logInPage;
            
        }
        [ObservableProperty]
        public bool isInitializing;

        public void Initiate()
        {

            throw new NotImplementedException();
        }
    }
}
