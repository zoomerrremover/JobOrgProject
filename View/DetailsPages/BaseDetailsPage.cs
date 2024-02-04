using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.View.DetailsPages;

public class BaseDetailsPage : ContentPage
{
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var BindingContextCasted = BindingContext as ThingVM;
        await Task.Run(BindingContextCasted.LoadContent);
    }
}