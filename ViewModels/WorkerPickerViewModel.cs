using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public ObservableCollection<Worker> WorkersPicked { get; set; } = new();

    ObservableCollection<Worker> workers { get => data.GetItems<Worker>(); }
    void UpdateList()
    {
        WorkersPicked.Clear();
        workers.Where(w => w.IsPicked == true).ToList().ForEach(w => WorkersPicked.Add(w));

    }
    [RelayCommand]
    void CheckBoxClicked(Worker worker)
    {
        worker.IsPicked = !worker.IsPicked;
        ObsWorkers.Remove(worker);
        ObsWorkers.Insert(0,worker);
        UpdateList();
    }


    public WorkerPickerViewModel(Initializator init, IDataStorage Storange)
    {
        init.Initialize();
        data = Storange;
        InitiateList();

    }
    async void InitiateList(string searchPromt = "")
    {
        ObsWorkers.Clear();
        searchPromt ??= "";
        var totalWorkers = workers.OrderByDescending(w => w.IsPicked);
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
        foreach (var worker in data.GetItems<Worker>())
        {
            worker.IsPicked = true;
        }
        InitiateList();
        UpdateList();
    }
    [RelayCommand]

    void UnSelectAll()
    {
        foreach (var worker in data.GetItems<Worker>())
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
