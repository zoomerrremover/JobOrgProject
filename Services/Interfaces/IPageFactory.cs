
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IPageFactory
    {
        public ContentPage MakePage(Thing model);

        public ContentPage MakeACreatePage(Type type);
    }
}
