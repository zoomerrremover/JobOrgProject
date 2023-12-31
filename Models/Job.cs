
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models.Interfaces;

namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Job:Thing,ITimeBased 
    {
        public List<Assignment> Tasks;

        public Contractor Contractor;

        public DateTime StartTime { get  ; set  ; }
        public DateTime FinishTime { get  ; set  ; }
        public Place Place { get  ; set  ; }
    }
}
