

using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class ScheldudeViewModel
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
