
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

        protected static IReflectionContent reflectionContent;

        [ObservableProperty]
        bool isLoading = false;

        async partial void OnIsLoadingChanged(bool value)
        {
            if (IsLoading)
            {
                await Task.Delay(300000);
                if (IsLoading)
                {
                    errorService.CallError("There have been unterminated loading task");
                    IsLoading = false;
                }
            }
        }
        public bool EditPermission { get; protected set; }

        protected Thing BindingObject;

        public static void SetStaticFields(IReflectionContent ReflectionContent,IErrorService ErrorService,IDataStorage DataStorage,IUserController UserController,IPageFactory PageFactory)
        {
            dataStorage = DataStorage;
            userController = UserController;
            pageFactory = PageFactory;
            errorService = ErrorService;
            reflectionContent = ReflectionContent;
        }
        public ModelView()
        {
        }
        protected void SetPermissions()
        {
            EditPermission = userController.GetPermission(BindingObject, RuleType.Edit);
        }
        public static ModelView CreateFromTheModel(Thing model)
        {
            throw new NotImplementedException();
        }

    }
}
