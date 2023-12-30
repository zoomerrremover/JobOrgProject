
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IConverter
    {
        BaseViewModel ConvertToViewModel(Thing model);
        DataTemplate ConvertToDataTemplate(Thing model);
        ContentPage ConvertToContentPage(Thing model);
    }
}
