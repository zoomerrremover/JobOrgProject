using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}