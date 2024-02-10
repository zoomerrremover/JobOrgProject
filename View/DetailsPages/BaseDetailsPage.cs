using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.View.DetailsPages;

public class BaseDetailsPage : ContentPage
{
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var BindingContextCasted = BindingContext as ThingVM;
        Loaded += LoadContentEvent;
        void LoadContentEvent(object sender,EventArgs e)
        {
            BindingContextCasted.LoadContent();
        }
    }
    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        var BindingContextCasted = BindingContext as ThingVM;
        BindingContextCasted.IsLoading = true;
        base.OnNavigatedFrom(args);
        BindingContextCasted.IsLoading = false;
    }
}