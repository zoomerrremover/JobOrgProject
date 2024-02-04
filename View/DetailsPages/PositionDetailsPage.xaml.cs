using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.DetailsViewModels;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Position))]
public partial class PositionDetailsPage : BaseDetailsPage
{
	public PositionDetailsPage()
	{
		InitializeComponent();
	}

}