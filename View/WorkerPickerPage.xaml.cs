namespace TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
public partial class WorkerPickerPage : ContentPage
{
	public WorkerPickerPage(WorkerPickerViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}