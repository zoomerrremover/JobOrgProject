
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services;

public class DataStorageTemp : BaseViewModel ,IDataStorage

{
    public ObservableCollection<Item> Items { get; set; }
    public ObservableCollection<WorkerUIL> Workers { get; set; }
    public ObservableCollection<Job> Jobs { get ; set ; }
    public ObservableCollection<Assignment> Assignments { get ; set ; }
    public ObservableCollection<Contractor> Contractors { get ; set ; }
    public ObservableCollection<Place> Places { get; set; }
    public List<Thing> this[string key]
    {
        get { switch (key)
            {
                case nameof(Item):
                    return Items.ToList().Select(w => (Thing)w).ToList();
                case nameof(Worker):
                    return Workers.ToList().Select(w => (Thing)w.Worker).ToList();
                case nameof(Job):
                    return Jobs.ToList().Select(w => (Thing)w).ToList();
                case nameof(Place):
                    return Places.ToList().Select(w => (Thing)w).ToList();
                case nameof(Contractor):
                    return Contractors.ToList().Select(w => (Thing)w).ToList();
                case nameof(Assignment):
                    return Assignments.ToList().Select(w => (Thing)w).ToList();
                default:
                    return new List<Thing>();


            }



        }
    }
    public DataStorageTemp()
    {
        Items = new();
        Workers = new();
        Jobs = new();
        Assignments = new();
        Contractors = new();
    }

}
