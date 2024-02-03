
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
public partial class AssignmentVM : ThingVM
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
    public async void Load(object vm) {
        var models = InitializeModels();
        WorkersCollectionView.Initiate(models,typeof(Worker));
        }

    async void Initialize()
    {
        TimeSelector = new(BindingObject);
        InitializeJobPicker();
        InitializePlacePicker();
        InitializeWorkersCollectionView();
    }
    #endregion
    #region TimeSelector
    public TimeBasedVM TimeSelector { get; set; }
    #endregion
    #region Pickable Job feature
    public StringPickerVM JobPicker { get; set; }

    void InitializeJobPicker()
    {

        var localJobs = dataStorage.GetItems<Job>();
        var initialValue = localJobs.Single(job => job.Tasks.Contains(BindingObject));
        var jobsCastedToThing = localJobs.Select(job => job as Thing).ToObservableCollection();
        JobPicker = new StringPickerVM(jobsCastedToThing, initialValue)
            .WithPermissions(EditPermission)
            .WithNoneOption()
            .WithAction(ChangeJobAction);
        void ChangeJobAction(string oldValue, string newValue)
        {
            var oldJob = oldValue != "None"?localJobs.Single(job => job.Name == oldValue):null;
            var newJob = newValue != "None" ? localJobs.Single(job => job.Name == newValue) : null;
            oldJob.Tasks.Remove(BindingObject);
            newJob.Tasks.Add(BindingObject);
        }
    }

    #endregion
    #region Displayable place
    public StringPickerVM PlacePicker { get; set; }
    void InitializePlacePicker()
    {

        var localPlaces = dataStorage.GetItems<Place>().Select(job => job as Thing).ToObservableCollection();
        var initialValue = BindingObject.Place;
        PlacePicker = new StringPickerVM(localPlaces, initialValue)
            .WithPermissions(EditPermission)
            .WithNoneOption()
            .WithAction(ChangeJobAction);
        void ChangeJobAction(string oldValue, string newValue)
        {
            var newPlace = newValue != "None" ? localPlaces.Single(job => job.Name == newValue) as Place : null;
            BindingObject.Place = newPlace;
        }
    }
    #endregion
    #region DisplayableWorkersFeature
    public ModelCollectionView WorkersCollectionView {  get; set; }

    [ObservableProperty]
    bool inEditMode = false;
    void ChangeEditMode() => InEditMode = !InEditMode;
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
        dataStorage.TriggerUpdate<Worker>();
    }

    private void InitializeWorkersCollectionView()
    {

        WorkersCollectionView = new ModelCollectionView()
            .WithEditButton(EditPermission, ChangeEditMode);
    }
    /// <summary>
    /// Initializes models into adaptor (for UI).
    /// </summary>
    List<PickableWorker> InitializeModels()
    {
        var ReturnWorkers = new List<PickableWorker>();
        PickableWorker.ActivatedAction = EditWorker;
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
