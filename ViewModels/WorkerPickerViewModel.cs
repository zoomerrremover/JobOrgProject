
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels;

public partial class WorkerPickerViewModel : BaseViewModel
{
    IDataStorage data;

    string mainEntryText = "";
    public string MainEntryText {
        get => mainEntryText;
        set {
            InitiateList(value);
            mainEntryText = value;
        } }
    SharedControls controls;
    [RelayCommand]
    void UpdateList()
    {
        data.Workers.Where(w => w.IsPicked == true).ToList().ForEach(w => controls.WorkersPicked.Add(w));

    }

    public WorkerPickerViewModel(IDataStorage Storange, SharedControls controls)
    {
        data = Storange;
        this.controls = controls;
        InitiateList();

    }
    void InitiateList(string searchPromt = "")
    {
        ObsWorkers.Clear();
        searchPromt ??= "";
        foreach (var worker in data.Workers)
        {
            if(worker.Worker.Name.Contains(searchPromt))
            {
                ObsWorkers.Add(worker);
            }
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
    public ObservableCollection<WorkerUIL> ObsWorkers { get; } = new();

}
