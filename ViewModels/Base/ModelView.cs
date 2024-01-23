
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.ViewModels.Base
{
    public abstract partial class ModelView:BaseViewModel
    {

        protected static IDataStorage dataStorage;

        protected static IUserController userController;

        protected static IPageFactory pageFactory;

        protected static IErrorService errorService;

        public bool EditPermission { get => userController.GetPermission(BindingObject, RuleType.Edit); }

        [ObservableProperty]
        Thing bindingObject;

        public static void SetStaticFields(IErrorService ErrorService,IDataStorage DataStorage,IUserController UserController,IPageFactory PageFactory)
        {
            dataStorage = DataStorage;
            userController = UserController;
            pageFactory = PageFactory;
            errorService = ErrorService;
        }
        public ModelView()
        {
             
        }
        public static ModelView CreateFromTheModel(Thing model)
        {
            throw new NotImplementedException();
        }

    }
}
