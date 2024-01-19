
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IPageFactory
    {
        public ContentPage MakeADetailsPage(Thing model);

        public ContentPage MakeACreatePage(Type type,Thing PreSets = null);
    }
}
