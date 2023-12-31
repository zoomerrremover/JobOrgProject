using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.View.DataTemplates;

[GlobalSearchDataTemplateAttribute(ClassLinked = typeof(Job))]
public partial class JobDataTemplate : DataTemplate
{
	public JobDataTemplate()
	{
		InitializeComponent();
	}
}