
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public interface IDataStorage
    {
        public List<Item> Items { get; set; }
        public List<Worker> Workers { get; set; }
        public List<Job> Jobs { get; set; }
        public List<JOTask> Tasks { get; set; }
        public List<Contractor> Contractors { get; set; }



    }
}
