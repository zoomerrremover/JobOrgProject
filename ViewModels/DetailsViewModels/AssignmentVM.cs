
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
public partial class AssignmentVM:ThingVM
{
    #region CTORS
    new public Assignment BindingObject { get; set; }
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Assignment)
        {
            var wm = new AssignmentVM(model as Assignment);
            return wm;
        }
        else return null;
    }
    public AssignmentVM(Assignment item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }
    private void Initialize()
    {
        var models = InitializeModels();
        DisplayableWorkers = new ModelCollectionView(models)
            .WithEditButton(EditPermission);
        TimeSelector = new(BindingObject);
        Jobs = dataStorage.GetItems<Job>();
        PickedJob = Jobs.Single(job=>job.Tasks.Contains(BindingObject));
    }
    #endregion
    #region TimeSelector
    public TimeBasedVM TimeSelector { get; set; }
    #endregion
    #region Pickable Job feature
    ObservableCollection<Job> Jobs { get; set; }
    public ObservableCollection<string> DisplayableJobs { get; set; }
    [ObservableProperty]
    string pickedJob;
    partial void OnPickedJobChanging(string oldValue, string newValue)
    {
        if(oldValue != "None")
        {
            Jobs()
        }
        oldValue.Tasks.Remove(BindingObject);
        newValue.Tasks.Add(BindingObject);
    }
    #endregion
    #region Displayable place
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
        dataStorage.TriggerUpdate<Job>();
        base.NameEditButtonPressed();
    }
    #endregion
    #region DisplayableWorkersFeature
    public ModelCollectionView DisplayableWorkers {  get; set; }
    [RelayCommand]
    /// <summary>
    /// Should be called when user have choosen worker.
     /// </summary>
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
        DisplayableWorkers.DisplayableList.TriggerEvent();
    }/// <summary>
     /// Initializes models into adaptor (for UI).
     /// </summary>

    List<PickableWorker> InitializeModels()
    {
        var ReturnWorkers = new List<PickableWorker>();
        var workers = dataStorage.GetItems<Worker>();
        foreach (var (worker, boolValue) in from worker in workers
                                            let NameLocal = worker.Name.ToLower()
                                            let boolValue = BindingObject.Workers.Contains(worker) ? true : false
                                            select (worker, boolValue))
        {
            ReturnWorkers.Add(new PickableWorker(worker, boolValue));
        }
        return ReturnWorkers;
    }

    #endregion
}
