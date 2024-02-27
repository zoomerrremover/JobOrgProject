using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp.View.MainPages;

public partial class LogInPage2 : ContentPage
{
	LogInViewModel logIn;

    public LogInPage2(LogInViewModel logIn)
	{
        Shell.SetTabBarIsVisible(this, false);
        BindingContext = logIn;
		this.logIn = logIn;
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await logIn.tryLoggingIn();
    }
}