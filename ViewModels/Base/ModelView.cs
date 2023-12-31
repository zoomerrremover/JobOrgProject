
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels.Base
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
