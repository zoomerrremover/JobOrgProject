
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
        Worker = worker;
        InitializeComponents();
    }
    [ObservableProperty]
    Worker worker;
    [ObservableProperty]
    LogicSwitch nameEditMode = new();
    [ObservableProperty]
    LogicSwitch descriptionEditMode = new();


    //Choosable postion feature
    //--------------------------------------------------------------------------
    [ObservableProperty]
    List<Position> possiblePositions;

    [ObservableProperty]
    string displayablePositionGet;
    public int DisplayablePositionSet
    {
        get
        {
            return PossiblePositions.IndexOf(Worker.Position);
        }
        set
        {
            Worker.Position = PossiblePositions[value];
            DisplayablePositionGet = PossiblePositions[value].Name;
        }
    }
    //Editable Description
    //--------------------------------------------------------------------------
    [ObservableProperty]
    string displayableDescriptionGet;
    public string DisplayableDescriptionSet
    {
        get { return DisplayableDescriptionGet; }
        set
        {
            DisplayableDescriptionGet = value;
            Worker.Description = value;
        }
    }
    //--------------------------------------------------------------------------
    //Current assignment feature
    ObservableCollection<Assignment> assignments = new();
    public ObservableCollection<Assignment> Assignments
    {
        get
        {
            assignments.Clear();
            foreach (var task in dataStorage.GetItems<Assignment>().Where(w => w.Workers.Contains(Worker)))
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


    public new void InitializeComponents()
    {
        DisplayablePositionGet = Worker.Position.Name;
        PossiblePositions = dataStorage.GetItems<Position>().ToList();
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

    public static bool Initialize(IDataStorage st)
    {
        dataStorage = st;
        return true;
    }




}
