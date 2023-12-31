using TheJobOrganizationApp.ViewModels;
using TheJobOrganizationApp.ViewModels.PopUpViewModels;

namespace TheJobOrganizationApp.View;

public partial class LogInPage : ContentPage
{
	public LogInPage(LogInViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}