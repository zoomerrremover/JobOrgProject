using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Assignment))]
public partial class AssignmentDetailsPage : BaseDetailsPage
{
	public AssignmentDetailsPage()
	{
		InitializeComponent();
	}
}