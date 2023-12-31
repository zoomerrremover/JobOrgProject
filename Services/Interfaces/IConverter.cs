
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IConverter
    {
        BaseViewModel ConvertToViewModel(Thing model);
        DataTemplate ConvertToDataTemplate(Thing model);
        ContentPage ConvertToContentPage(Thing model);
        DataTemplate ConvertToDataTemplate(Type model);
    }
}
