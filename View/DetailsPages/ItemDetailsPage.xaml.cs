using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.View.Controls;
[DetailsPage(ClassLinked = typeof(Item))]
public partial class ItemDetails : ContentPage
{
	public ItemDetails()
	{
		InitializeComponent();
	}
}