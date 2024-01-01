
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels.Base
{
    public class ModelView:BaseViewModel
    {

        protected static IDataStorage queryService;
        public ModelView(IDataStorage QueryService)
        {
            queryService = QueryService;
        }
        public static ModelView CreateFromTheModel(Thing model)
        {
            throw new NotImplementedException();
        }
        public ModelView()
        {
            
        }
    }
}
