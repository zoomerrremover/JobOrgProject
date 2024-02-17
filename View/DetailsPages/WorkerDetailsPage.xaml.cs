using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.View.DetailsPages;
using TheJobOrganizationApp.ViewModels.DetailsViewModels;

namespace TheJobOrganizationApp.View.Controls;
[DetailsPage(ClassLinked = typeof(Worker))]
public partial class WorkerDetails : BaseDetailsPage
{
	public WorkerDetails()
	{
		InitializeComponent();
	}
}