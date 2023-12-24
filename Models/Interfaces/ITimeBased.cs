namespace TheJobOrganizationApp.Models.Interfaces
{
    public interface ITimeBased
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Place Place { get; set; }
    }
}
