
using CommunityToolkit.Mvvm.Input;

namespace TheJobOrganizationApp.ViewModels;

public partial class LogInViewModel: BaseViewModel
{

    [RelayCommand]
    async Task goToScheldude()
    {
        await Shell.Current.GoToAsync("..");//{nameof(ScheldudePage)}
    }
}
