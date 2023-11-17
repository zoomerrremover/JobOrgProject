
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
        controls.WorkersPicked.ToList().ForEach(w =>
        {
            var tasks = Data.Tasks.Where(t => t.Workers.Contains(w.Worker));
            foreach (var task in tasks)
            {
                if (controls.tasksOnTheScreen.Contains(task.Id)) {
                    controls.tasksOnTheScreen.Add(task.Id);
                    var newAppointment = new SchedulerAppointment { StartTime = task.StartTime, EndTime = task.FinishTime, Subject = task.Name , Background = w.Worker.Color};
                    controls.appointments.Add(newAppointment);
                }
            }
        });

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
