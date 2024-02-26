using TheJobOrganizationApp.ViewModels;
using TheJobOrganizationApp.ViewModels.MainViewModels;
using TheJobOrganizationApp.ViewModels.PopUpViewModels;

namespace TheJobOrganizationApp.View;

public partial class LogInPage : ContentPage
{
	public LogInPage(LogInViewModel bc)
	{
        Shell.SetTabBarIsVisible(this, false);
        BindingContext = bc;
		InitializeComponent();
	}
}