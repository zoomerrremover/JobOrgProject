
using CommunityToolkit.Mvvm.Input;

namespace TheJobOrganizationApp.ViewModels;

public partial class LogInViewModel: BaseViewModel
{
    public LogInViewModel(ScheldudeViewModel contextToPush)
    {
        this.contextToPush = contextToPush;
    }

    [RelayCommand]
    async Task goToScheldude()
    {
        await Shell.Current.GoToAsync("..");//{nameof(ScheldudePage)}
    }
}
