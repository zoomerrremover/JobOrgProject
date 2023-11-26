using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class WorkerDetailPage : ContentPage
{
	public WorkerDetailPage(WorkerDetailsViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}