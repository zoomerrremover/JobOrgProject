using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.View.DataTemplates;

[GlobalSearchDataTemplateAttribute(ClassLinked = typeof(Worker))]
partial class WorkerDataTemplate : DataTemplate
{
	public WorkerDataTemplate()
	{
		InitializeComponent();
	}
}