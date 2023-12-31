using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.Controls;

[DetailsPage(ClassLinked = typeof(Assignment))]
public partial class AssignmentDetails : ContentPage
{
	public AssignmentDetails()
	{
		InitializeComponent();
	}
}