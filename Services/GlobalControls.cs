
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class GlobalControls
    {
        public List<Worker> WorkersPicked { get; set; }

        public GlobalControls()
        {
            WorkersPicked = new List<Worker>();
        }
    }
}
