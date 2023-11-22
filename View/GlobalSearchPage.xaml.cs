namespace TheJobOrganizationApp.View;

public partial class GlobalSearchPage : ContentPage
{
	public GlobalSearchPage(GlobalSearchPage bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}