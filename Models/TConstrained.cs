
namespace TheJobOrganizationApp.Models
{
    public abstract class TConstrained: Thing
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Place Place { get; set; }
    }
}
