using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.View.DetailsPages;
using TheJobOrganizationApp.ViewModels.DetailsViewModels;

namespace TheJobOrganizationApp.View.Controls;
[DetailsPage(ClassLinked = typeof(Item))]
public partial class ItemDetails : BaseDetailsPage
{
	public ItemDetails()
	{
		InitializeComponent();
	}

}