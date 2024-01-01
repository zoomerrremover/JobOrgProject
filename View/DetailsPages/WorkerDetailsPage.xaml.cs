using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.Controls;
[DetailsPage(ClassLinked = typeof(Worker))]
public partial class WorkerDetails : ContentPage
{
	public WorkerDetails()
	{
		InitializeComponent();
	}
}