
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels
{
    [QueryProperty("Worker", "Worker")]
    public partial class WorkerDetailsViewModel:BaseViewModel
    {
        [ObservableProperty]
        Worker worker;

        IDataStorage ds;

        public ObservableCollection<Assignment> Assignments { get
            {
                var totalAssgnments = ds.GetItems<Assignment>();
                ObservableCollection<Assignment> assignments = new ObservableCollection<Assignment>();
                totalAssgnments.Where(w => w.Workers.Contains(Worker)).ToList().ForEach(assignments.Add);
                return assignments;
            } 
        }

        public WorkerDetailsViewModel(IDataStorage ds)
        {
            this.ds = ds;       
        }
    }
}
