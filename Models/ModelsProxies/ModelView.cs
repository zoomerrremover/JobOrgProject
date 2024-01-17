
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.ViewModels;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    public class ModelView:BaseViewModel
    {

        protected static IDataStorage queryService;
        public static ModelView CreateFromTheModel(Thing model)
        {
            throw new NotImplementedException();
        }
    }
}
