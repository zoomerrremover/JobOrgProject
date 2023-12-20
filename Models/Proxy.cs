
namespace TheJobOrganizationApp.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class Proxy
        :Attribute
    {
        public Type ClassLinked { get; set; }
    }
}
