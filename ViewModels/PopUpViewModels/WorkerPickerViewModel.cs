
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.PopUpViewModels;

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
    ObservableCollection<Worker> Workers;
    void UpdateList()
    {
        WorkersPicked.Clear();
        Workers.Where(w => w.IsPicked == true).ToList().ForEach(WorkersPicked.Add);

    }
    [RelayCommand]
    void CheckBoxClicked(Worker worker)
    {
        worker.IsPicked = !worker.IsPicked;
        ObsWorkers.Remove(worker);
        ObsWorkers.Insert(0,worker);
        UpdateList();
    }


    public WorkerPickerViewModel( IDataStorage Storange)
    {
        data = Storange;
        Workers = data.GetItems<Worker>();
        Workers.CollectionChanged += UpdateList;
        InitiateList();

    }

    private void UpdateList(object sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateList();
    }

    async void InitiateList(string searchPromt = "")
    {
        ObsWorkers.Clear();
        searchPromt ??= "";
        var totalWorkers = Workers.OrderByDescending(w => w.IsPicked);
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
