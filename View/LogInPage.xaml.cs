using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class LogInPage : ContentPage
{
	public LogInPage(LogInViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}