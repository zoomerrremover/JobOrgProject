
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public override void LoadContent()
    {
        base.LoadContent();
        InitializeTimeSelector();
        InitializeWorkerPicker();
        InitializeJobPicker();
        InitializePlacePicker();
    }
    private void InitializeWorkerPicker()
    {
        var models = InitializeModels();
        WorkersCollectionView.Initiate(models, typeof(Worker));
    }

    void Initialize()
    {
        var localJobs = dataStorage.GetItems<Job>();
        TimeSelector = new(BindingObject);
        JobPicker = new StringPickerVM()
                        .WithPermissions(EditPermission)
                            .WithNoneOption()
                                .WithAction(ChangeJobAction);

        void ChangeJobAction(string oldValue, string newValue)
        {
            if (EditPermission && !IsLoading)
            {
                var oldJob = oldValue != "None" ? localJobs.Where(job => job.Name == oldValue).FirstOrDefault() : null;
                var newJob = newValue != "None" ? localJobs.Where(job => job.Name == newValue).FirstOrDefault() : null;
                if(oldJob != null) oldJob.Tasks.Remove(BindingObject);
                if (newJob != null) newJob.Tasks.Add(BindingObject);
                CreateChangeHistoryRecord("job", oldValue, newValue);
            }
        }
        var localPlaces = dataStorage.GetItems<Place>().Select(job => job as Thing).ToObservableCollection();
        PlacePicker = new StringPickerVM()
                        .WithPermissions(EditPermission)
                            .WithNoneOption()
                                .WithAction(ChangePlaceAction);
        void ChangePlaceAction(string oldValue, string newValue)
        {
            if (EditPermission && !IsLoading)
            {
                var newPlace = newValue != "None" ? localPlaces.Where(job => job.Name == newValue).FirstOrDefault() as Place : null;
                BindingObject.Place = newPlace;
                CreateChangeHistoryRecord("location",oldValue,newValue);
            }
        }
        WorkersCollectionView = new ModelCollectionView()
                                    .WithEditButton(EditPermission, ChangeEditMode);
    }
    #endregion
    #region TimeSelector
    void InitializeTimeSelector()
    {
        TimeSelector.InitializeData();
    }
    public TimeBasedVM TimeSelector { get; set; }
    #endregion
    #region Pickable Job feature
    public StringPickerVM JobPicker { get; set; }
    void InitializeJobPicker()
    {
        var localJobs = dataStorage.GetItems<Job>();
        Job initialValue = localJobs.Where(job => job.Tasks.Contains(BindingObject)).FirstOrDefault();
        var jobsCastedToThing = localJobs.Select(job => job as Thing);
        JobPicker.InitializeContent(jobsCastedToThing, initialValue);
    }
    #endregion
    #region Displayable place
    public StringPickerVM PlacePicker { get; set; }
    void InitializePlacePicker()
    {
        var localPlaces = dataStorage.GetItems<Place>().Select(job => job as Thing).ToObservableCollection();
        var initialValue = BindingObject.Place;
        PlacePicker.InitializeContent(localPlaces, initialValue);
    }
    #endregion
    #region DisplayableWorkersFeature
    public ModelCollectionView WorkersCollectionView {  get; set; }

    [ObservableProperty]
    bool inEditMode = false;
    void ChangeEditMode() => InEditMode = !InEditMode;
    /// <summary>
    /// Should be called when user have choosen worker.
    /// </summary>
    [RelayCommand]
    async void EditWorker(PickableWorker obj)
    {
        if (EditPermission && !IsLoading && InEditMode)
        {
            IsLoading = true;
            obj.data = !obj.data;
            if (obj.data)
            {
                BindingObject.Workers.Add(obj.model);
            }
            else
            {
                BindingObject.Workers.Remove(obj.model);
            }
            CreateChangeHistoryRecord("worker list");
            await Task.Run(InitializeWorkerPicker);
            IsLoading = false;
        }
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
