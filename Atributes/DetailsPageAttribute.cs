
namespace TheJobOrganizationApp.Atributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DetailsPageAttribute
    : Attribute
{
    public Type ClassLinked { get; set; }
}
