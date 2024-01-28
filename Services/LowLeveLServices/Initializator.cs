
using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.PreBaked.PopupPages.Login;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.Services.LowLeveLServices
{
    public partial class Initializator : ObservableObject,IInitializator
    {
        LoginViewModel loginViewModel;
        LogInPage logInPage;
        IConnectionService connectionService;
        IErrorService errorService;
        IUserController UserController;
        IDataStorage DataStorange;
        IReflectionContent refContent;
        public Initializator(IReflectionContent refContent,IDataStorage DataStorange,IUserController UserController,IErrorService errorService,IConnectionService ConnectionService,LogInPage logInPage)
        {
            this.refContent = refContent;
            this.errorService = errorService;
            this.DataStorange = DataStorange;
            this.UserController = UserController;
            connectionService = ConnectionService;
            loginViewModel = logInPage.BindingContext as LoginViewModel;
            this.logInPage = logInPage;
            Initiate();
            
        }
        [ObservableProperty]
        public bool isInitializing;

        public void Initiate()
        {
            connectionService.LoadContent();
            var user = DataStorange.GetItems<Worker>().First();
            List<Rule> rules = new List<Rule>();
            var permissions = new List<RuleType> { RuleType.Delete, RuleType.Create, RuleType.Edit };
            foreach(var model in refContent.Models)
            {
                var rule = new Rule() { Model = model, Status = permissions };
                rules.Add(rule);
            }
            Position position = new Position() { Name = "Admin",Permissions=rules};
            DataStorange.AddThing(position);
            user.Position = position;
            UserController.SetPermissions(user);
          // App Starting logic
        }
    }
}
