using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class GlobalSearchPage : ContentPage
{
	public GlobalSearchPage(GlobalSearchViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}