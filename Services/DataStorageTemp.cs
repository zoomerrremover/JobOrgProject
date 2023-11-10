
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services;

public class DataStorageTemp : IDataStorage

{
    public List<Item> Items { get; set; }
    public List<Worker> Workers { get; set; }
    public List<Job> Jobs { get ; set ; }
    public List<JOTask> Tasks { get ; set ; }
    public List<Contractor> Contractors { get ; set ; }

    public DataStorageTemp()
    {
        Items = new();
        Workers = new();
        Jobs = new();
        Tasks = new();
        Contractors = new();
    }

}
