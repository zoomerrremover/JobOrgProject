
namespace TheJobOrganizationApp.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class Model
        :Attribute
    {
        public bool DisplayableInTheGlobalSearch {  get; set; }
    }
}
