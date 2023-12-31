
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DataTemplates;

[GlobalSearchDataTemplateAttribute(ClassLinked = typeof(Assignment))]
public partial class AssignmentDataTemplate : DataTemplate
{
	public AssignmentDataTemplate()
	{
		InitializeComponent();
	}
}