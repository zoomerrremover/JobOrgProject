
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels;

public partial class WorkerPickerViewModel : BaseViewModel
{
    IDataStorage Data;
    public WorkerPickerViewModel(IDataStorage Storange)
    {
        Data = Storange;
        InitiateList();

    }
    void InitiateList()
    {
    ObsWorkers.Clear();
        foreach (var worker in Data.Workers)
        {
        ObsWorkers.Add(worker);
        }
    }
    public ObservableCollection<Worker> ObsWorkers { get; } = new();

}
