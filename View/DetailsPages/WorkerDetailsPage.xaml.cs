using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.DetailsViewModels;

namespace TheJobOrganizationApp.View.Controls;
[DetailsPage(ClassLinked = typeof(Worker))]
public partial class WorkerDetails : ContentPage
{
	public WorkerDetails()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        WorkerVM.Load(BindingContext);
    }
}