
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class GlobalControls
    {
        public List<WorkerUIL> WorkersPicked { get; set; }

        public GlobalControls(IDataStorage storange)
        {
            WorkersPicked = new ();
            WorkersPicked = storange.Workers.Select(w => (WorkerUIL)w).ToList();
        }
    }
}
