
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Job:TConstrained 
    {
        public List<Assignment> Tasks;

        public Contractor Contractor;
    }
}
