

using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public partial class ScheldudeViewModel
    : BaseViewModel
{
    async Task GoToLogInScreen()
    {
        await Shell.Current.GoToAsync($"{nameof(LogInPage)}");
    }
    public ScheldudeViewModel()
    {
          GoToLogInScreen();
    }
}
