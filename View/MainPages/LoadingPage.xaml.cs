using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp.View.MainPages;

public partial class LoadingPage : ContentPage
{
	LoadingPageViewModels vm;

    public LoadingPage(LoadingPageViewModels vm)
	{
		BindingContext = vm;
		this.vm = vm;
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        Shell.SetTabBarIsVisible(this, false);
        base.OnAppearing();
		await vm.StartLoading();
    }
}
