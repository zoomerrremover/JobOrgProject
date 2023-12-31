using TheJobOrganizationApp.ViewModels;
using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}