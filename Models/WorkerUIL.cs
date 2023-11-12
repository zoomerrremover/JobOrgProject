


namespace TheJobOrganizationApp.Models
{
    public class WorkerUIL
    {
        public bool IsPicked {  get; set; }
         
        public Worker Worker { get; set; }

        public static explicit operator WorkerUIL(Worker worker)
        {
            return new WorkerUIL {Worker=worker,IsPicked=true};
        }
    }
}
