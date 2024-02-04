using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.DetailsViewModels;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Job))]
public partial class JobDetailsPage : BaseDetailsPage
{
	public JobDetailsPage()
	{
		InitializeComponent();
	}

}