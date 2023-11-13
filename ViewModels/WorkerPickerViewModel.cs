
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels;

public partial class WorkerPickerViewModel : BaseViewModel
{
    IDataStorage Data;

    GlobalControls controls;

    public WorkerPickerViewModel(IDataStorage Storange, GlobalControls controls)
    {
        Data = Storange;
        this.controls = controls;
        controls.WorkersPicked = ObsWorkers.Where(x => x.IsPicked == true).ToList();

        InitiateList();

    }
    void InitiateList()
    {
        ObsWorkers.Clear();
        foreach (var worker in Data.Workers)
        {
            ObsWorkers.Add((WorkerUIL)worker);
        }
    }
    //public void WorkerChoosen(object sender,CheckedChangedEventArgs worker)
    //{

    //    if(contorls.WorkersPicked.Contains(worker))
    //    {
    //        throw new Exception("Logic error");
    //    }
    //    contorls.WorkersPicked.Add(worker);
    //}
    public IEnumerable<Worker> Workers { get; private set; }
    public ObservableCollection<WorkerUIL> ObsWorkers { get; } = new();

}
