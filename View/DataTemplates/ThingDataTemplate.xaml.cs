using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DataTemplates;

[GlobalSearchDataTemplateAttribute(ClassLinked = typeof(Thing))]
public partial class ThingDataTemplate : DataTemplate
{
	public ThingDataTemplate()
	{
		InitializeComponent();
	}
}