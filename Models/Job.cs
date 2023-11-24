
namespace TheJobOrganizationApp.Models
{
    public class Job:TConstrained 
    {
        public List<Assignment> Tasks;

        public Contractor Contractor;
    }
}
