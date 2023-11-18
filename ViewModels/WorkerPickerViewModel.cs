
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels;

public partial class WorkerPickerViewModel : BaseViewModel
{
    [ObservableProperty]

    IDataStorage data;

    GlobalControls controls;
    [RelayCommand]
    void UpdateList()
    {
        Data.Workers.Where(w => w.IsPicked == true).ToList().ForEach(w => controls.WorkersPicked.Add(w));

    }

    public WorkerPickerViewModel(IDataStorage Storange, GlobalControls controls)
    {
        Data = Storange;
        this.controls = controls;
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
