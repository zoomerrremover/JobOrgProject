using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DataTemplates;

[GlobalSearchDataTemplateAttribute(ClassLinked = typeof(Place))]
public partial class PlaceDataTemplate : DataTemplate
{
	public PlaceDataTemplate()
	{
		InitializeComponent();
	}
}