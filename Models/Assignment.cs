
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Assignment:TConstrained
    {
        public List<Worker> Workers;
        
    }
}
