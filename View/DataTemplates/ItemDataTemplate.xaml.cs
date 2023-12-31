using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DataTemplates;

[GlobalSearchDataTemplateAttribute(ClassLinked = typeof(Item))]
public partial class ItemDataTemplate : DataTemplate
{
	public ItemDataTemplate()
	{
		InitializeComponent();
	}
}