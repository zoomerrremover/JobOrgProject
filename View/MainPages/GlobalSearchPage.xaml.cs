
using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp.View;

public partial class GlobalSearchPage : ContentPage
{
	public GlobalSearchPage(GlobalSearchViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
    }
}