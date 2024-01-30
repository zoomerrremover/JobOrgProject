using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Contractor))]
public partial class ContractorDetailsPage : ContentPage
{
	public ContractorDetailsPage()
	{
		InitializeComponent();
	}
}