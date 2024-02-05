
using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp.View;

public partial class GlobalSearchPage : ContentPage
{
	public GlobalSearchPage(GlobalSearchViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var vm = BindingContext as GlobalSearchViewModel;
        vm.LoadContent();
    }
}