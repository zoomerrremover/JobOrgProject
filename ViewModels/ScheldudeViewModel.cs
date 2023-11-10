

using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public partial class ScheldudeViewModel
    : BaseViewModel
{
    public ScheldudeViewModel(IAPIService apiservice)
    {
        
    }
    async Task GoToLogInScreen()
    {
        await Shell.Current.GoToAsync($"{nameof(LogInPage)}");
    }
    public ScheldudeViewModel()
    {
          GoToLogInScreen();
    }
}
