
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services
{
    public interface IDataStorage
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<WorkerUIL> Workers { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        public ObservableCollection<JOTask> Tasks { get; set; }
        public ObservableCollection<Contractor> Contractors { get; set; }



    }
}
