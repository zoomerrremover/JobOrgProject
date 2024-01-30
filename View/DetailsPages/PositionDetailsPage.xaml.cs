using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Position))]
public partial class PositionDetailsPage : ContentPage
{
	public PositionDetailsPage()
	{
		InitializeComponent();
	}
}