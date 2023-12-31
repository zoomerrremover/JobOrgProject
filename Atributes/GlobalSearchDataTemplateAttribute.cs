
namespace TheJobOrganizationApp.Atributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class GlobalSearchDataTemplateAttribute
    : Attribute
{
    public Type ClassLinked { get; set; }
}
