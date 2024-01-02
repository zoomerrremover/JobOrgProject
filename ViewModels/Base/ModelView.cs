
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.ViewModels.Base
{
    public abstract class ModelView:BaseViewModel
    {

        public static IDataStorage queryService;

        public static IUserController userController;
        public static ModelView CreateFromTheModel(Thing model)
        {
            throw new NotImplementedException();
        }
        public ModelView()
        {
            
        }
    }
}
