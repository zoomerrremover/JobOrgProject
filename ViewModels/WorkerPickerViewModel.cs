
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Layouts;
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
        controls.WorkersPicked.Clear();
        data.Workers.Where(w => w.IsPicked == true).ToList().ForEach(w => controls.WorkersPicked.Add(w));

    }

    public WorkerPickerViewModel(IDataStorage Storange, SharedControls controls)
    {
        data = Storange;
        this.controls = controls;
        InitiateList();

    }
    async void InitiateList(string searchPromt = "")
    {
        ObsWorkers.Clear();
        searchPromt ??= "";
        var totalWorkers = data.Workers.OrderByDescending(w => w.IsPicked);
        foreach (var worker in totalWorkers)
        {
            if (worker.Name.Contains(searchPromt))
            {
                ObsWorkers.Add(worker);
            }
        }
    }
    [RelayCommand]

    void SelectAll()
    {
        foreach (var worker in data.Workers)
        {
            worker.IsPicked = true;
        }
        InitiateList();
        UpdateList();
    }
    [RelayCommand]

    void UnSelectAll()
    {
        foreach (var worker in data.Workers)
        {
            worker.IsPicked = false;
        }
        InitiateList();
        UpdateList();
    }
    //public void WorkerChoosen(object sender,CheckedChangedEventArgs worker)
    //{

    //    if(contorlssPicked.Contains(worker))
    //    {
    //        throw new Exception("Logic error");
    //    }
    //    contorlssPicked.Add(worker);
    //}
    public ObservableCollection<Worker> ObsWorkers { get; set; } = new();

}
