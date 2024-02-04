
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.PopUpViewModels;

public partial class LogInViewModel: BaseViewModel
{


    [RelayCommand]
    async Task goToScheldude()
    {
        await Shell.Current.GoToAsync("..");//{nameof(ScheldudePage)}
    }
}
