
using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp;

public partial class ScheldudePage : ContentPage
{

    public ScheldudePage(ScheldudeViewModel bc)
    {
        Shell.SetTabBarIsVisible(this, true);
        BindingContext = bc;
        InitializeComponent();

    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var bc = BindingContext as ScheldudeViewModel;
        await Task.Run(bc.InitializeAppointments);
    }


}