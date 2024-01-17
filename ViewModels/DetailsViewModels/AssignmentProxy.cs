
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;
using TheJobOrganizationApp.ViewModels.ModelWrappers;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;

[DetailsViewModel(ClassLinked = typeof(Assignment))]
public partial class AssignmentDetailsVM:ThingDetailsVM
{
    new public Assignment BindingObject { get; set; }
    // CTORS
    //------------------------------------------------------------------------------------------------
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Assignment)
        {
            var wm = new AssignmentDetailsVM(model as Assignment);
            return wm;
        }
        else return null;
    }
    public AssignmentDetailsVM(Assignment item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }
    private void Initialize()
    {
        var models = InitializeModels();
        var editPermission = userController.GetPermission(BindingObject, RuleType.Edit);
        DisplayableWorkers = new ModelCollectionView(models)
            .WithEditButton(editPermission);
        Jobs = new ObservableCollection<Job>();
        TimeSelector = new(BindingObject);
        var jobsLoaded = queryService.GetItems<Job>();
        jobsLoaded.ForEach(Jobs.Add);
    }
    #region TimeSelector
    public TimeBasedVM TimeSelector { get; set; }
    #endregion
    #region Pickable Job feature
    public ObservableCollection<Job> Jobs { get; set; }
    [ObservableProperty]
    Job pickedJob;
    partial void OnPickedJobChanging(Job oldValue, Job newValue)
    {
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
        queryService.TriggerUpdate<Job>();
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
        var workers = queryService.GetItems<Worker>();
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
