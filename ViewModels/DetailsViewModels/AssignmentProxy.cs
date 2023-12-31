
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;
using TheJobOrganizationApp.ViewModels.ModelWrappers;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;

[DetailsViewModel(ClassLinked = typeof(Assignment))]
public partial class AssignmentProxy:ThingProxy
{
    new public Assignment BindingObject { get; set; }
    // CTORS
    //------------------------------------------------------------------------------------------------
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Assignment)
        {
            var wm = new AssignmentProxy(model as Assignment);
            return wm;
        }
        else return null;
    }
    public AssignmentProxy(Assignment item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }

    private void Initialize()
    {

        workers = queryService.GetItems<Worker>();
    }
    //Pickable job feature
    //------------------------------------------------------------------------------------------------
    [ObservableProperty]
    ObservableCollection<Job> jobs;
    [ObservableProperty]
    Job pickedJob;
    //Pickable place feature
    //------------------------------------------------------------------------------------------------
    [ObservableProperty]
    ObservableCollection<Place> places;
    [ObservableProperty]
    Place pickedPlace;
    protected override void NameEditButtonPressed()
    {
        foreach(var job in Jobs)
        {
            job.Tasks.Remove(BindingObject);
        }
        BindingObject.Place = PickedPlace;
        PickedJob.Tasks.Add(BindingObject);
        queryService.TriggerUpdate<Job>();
        base.NameEditButtonPressed();
    }
    //Displayable workers feature
    //------------------------------------------------------------------------------------------------
    [ObservableProperty]
    bool workersInEditMode = true;
    [RelayCommand]
    void EditWorker(PickableWorker obj)
    {
        obj.data = !obj.data;
        if (obj.data)
        {
            BindingObject.Workers.Add(obj.model);
        }
        else
        {
            BindingObject.Workers.Remove(obj.model);
        }
        LoadModels();
    }
    [RelayCommand]
    void OnEditButtonPressed()
    {
        WorkersInEditMode = !WorkersInEditMode;
        if (!WorkersInEditMode)
        {
            queryService.TriggerUpdate<Worker>();
        }
    }
    [ObservableProperty]
    ObservableCollection<PickableWorker> displayableWorkers = new();
    ObservableCollection<Worker> workers = new ObservableCollection<Worker>();
    [ObservableProperty]
    string searchEntryText = "";
    partial void OnSearchEntryTextChanged(string value)
    {
        LoadModels();
    }
    void LoadModels()
        {
        var promptLocal = SearchEntryText.ToLower();
        foreach (var (worker, boolValue) in from worker in workers
                                            let NameLocal = worker.Name.ToLower()
                                            where NameLocal.Contains(promptLocal)
                                            let boolValue = BindingObject.Workers.Contains(worker) ? true : false
                                            select (worker, boolValue))
        {
            DisplayableWorkers.Add(new PickableWorker(worker, boolValue));
        }
    }

}
