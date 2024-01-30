using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Job))]
public partial class JobDetailsPage : ContentPage
{
	public JobDetailsPage()
	{
		InitializeComponent();
	}
}