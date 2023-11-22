namespace TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
public partial class WorkerPickerPage
{
	public WorkerPickerPage(WorkerPickerViewModel bc)
	{
		BindingContext = bc;
		InitializeComponent();
	}
}