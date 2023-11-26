
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels
{
    [QueryProperty("Worker", "Worker")]
    public partial class WorkerDetailsViewModel:BaseViewModel
    {
        [ObservableProperty]
        Worker worker;

        public WorkerDetailsViewModel()
        {
                
        }
    }
}
