
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.ViewModels.BindableControls;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;
[DetailsViewModel(ClassLinked = typeof(Worker))]
public partial class WorkerVM:ThingVM
{
    #region CTORS
    new public Worker BindingObject { get; set; }
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Worker)
        {
            var wm = new WorkerVM(model as Worker);
            return wm;
        }
        else return null;
    }

    public WorkerVM(Worker worker):base(worker)
    {
        BindingObject = worker;
        Initialize();
    }
    public void Initialize()
    {
        InitializeCurrentAssignment();
        InitializePositionPicker();
        InitializeHasItemsVM();
        InitializeContactsVM();
    }
    #endregion
    #region Position
    public StringPickerVM PositionPickerVM { get; set; }
    void InitializePositionPicker()
    {

        var localPositions = dataStorage.GetItems<Position>().Select(job => job as Thing).ToObservableCollection();
        var initialValue = BindingObject.Position;
        PositionPickerVM = new StringPickerVM(localPositions, initialValue)
            .WithPermissions(EditPermission)
            .WithNoneOption()
            .WithAction(ChangeJobAction);
        void ChangeJobAction(string oldValue, string newValue)
        {
            var newPlace = newValue != "None" ? localPositions.Single(job => job.Name == newValue) as Position: null;
            BindingObject.Position = newPlace;
        }
    }
    #endregion
    #region Current Assignment
    ObservableCollection<Assignment> assignments = new();
    public ObservableCollection<Assignment> Assignments
    {
        get
        {
            assignments.Clear();
            foreach (var task in dataStorage.GetItems<Assignment>().Where(w => w.Workers.Contains(BindingObject)))
            {
                assignments.Add(task);
            }
            return assignments;

        }
    }
    [ObservableProperty]
    Assignment currentTask;
    [ObservableProperty]
    Assignment nextTask;
    private void InitializeCurrentAssignment()
    {
        NextTask = Assignments.Where(a => a.StartTime < DateTime.Now).OrderBy(a => a.StartTime).First();
        CurrentTask = Assignments.Where(w => w.StartTime <= DateTime.Now && w.FinishTime >= DateTime.Now).First();
    }
    public string TaskName
    {
        get => CurrentTask is null ? "None , Next task" : CurrentTask.Name;
    }
    public string SecondaryText
    {
        get => CurrentTask is null ? NextTask.Name : dataStorage.GetItems<Job>().Where(j => j.Tasks.Contains(CurrentTask)).First().Name;
    }
    public string Addressline
    {
        get => CurrentTask is null ? NextTask.Place.Name : CurrentTask.Place.Name;
    }
    public bool IsCurrentTaskActive { get => CurrentTask is null ? false : true; }

    public string BStartTime { get {
                                var task = CurrentTask is null ? NextTask.StartTime : CurrentTask.StartTime;
                                return task.ToString("hh:mm"); }
                              }
    public string BFinishTime 
    {
        get
        {  
            var task = CurrentTask is null ? NextTask.FinishTime : CurrentTask.FinishTime;
            return task.ToString("hh:mm");
        }
    }
    #endregion
    #region Items
    public HasItemsVM HasItemsVM { get; set; }

    void InitializeHasItemsVM()
    {
        HasItemsVM = new(BindingObject);
    }
    #endregion
    #region Contacts
    HasContactsVM HasContactsVM { get; set; }
    void InitializeContactsVM()
    {
        HasContactsVM = new(BindingObject);
    }
    #endregion
}
