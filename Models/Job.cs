
namespace TheJobOrganizationApp.Models
{
    public class Job:TConstrained 
    {
        public List<JOTask> Tasks;

        public Contractor Contractor;
    }
}
