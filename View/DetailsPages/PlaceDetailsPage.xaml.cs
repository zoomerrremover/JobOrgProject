using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DetailsPages;
[DetailsPage(ClassLinked = typeof(Place))]
public partial class PlaceDetailsPage : ContentPage
{
	public PlaceDetailsPage()
	{
		InitializeComponent();
	}
}