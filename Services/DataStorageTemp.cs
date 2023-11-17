
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services;

public class DataStorageTemp : BaseViewModel ,IDataStorage

{
    public ObservableCollection<Item> Items { get; set; }
    public ObservableCollection<WorkerUIL> Workers { get; set; }
    public ObservableCollection<Job> Jobs { get ; set ; }
    public ObservableCollection<JOTask> Tasks { get ; set ; }
    public ObservableCollection<Contractor> Contractors { get ; set ; }

    public DataStorageTemp()
    {
        Items = new();
        Workers = new();
        Jobs = new();
        Tasks = new();
        Contractors = new();
    }

}
