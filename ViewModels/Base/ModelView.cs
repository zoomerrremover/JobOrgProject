
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.ViewModels.Base
{
    public abstract class ModelView:BaseViewModel
    {

        protected static IDataStorage dataStorage;

        protected static IUserController userController;

        protected static IPageFactory pageFactory;

        public static void SetStaticFields(IDataStorage DataStorage,IUserController UserController,IPageFactory PageFactory)
        {
            dataStorage = DataStorage;
            userController = UserController;
            pageFactory = PageFactory;
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
