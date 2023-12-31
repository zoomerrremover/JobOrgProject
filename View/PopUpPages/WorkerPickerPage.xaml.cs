namespace TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using TheJobOrganizationApp.ViewModels.PopUpViewModels;

public partial class WorkerPickerPage
{
	public WorkerPickerPage(WorkerPickerViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}