using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.DetailsViewModels;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Contractor))]
public partial class ContractorDetailsPage : BaseDetailsPage
{
	public ContractorDetailsPage()
	{
		InitializeComponent();
	}

}