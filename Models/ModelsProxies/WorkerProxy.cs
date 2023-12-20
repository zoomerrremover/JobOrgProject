
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Models.ModelsProxies;
public partial class WorkerProxy:ModelView
{
    static IDataStorage queryService;
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Worker)
        {
            var wm = new WorkerProxy(model as Worker);
            return wm;
        }
        else return null;
    }
    public WorkerProxy(Worker worker)
    {
        Worker = worker;
        InitializeComponents();
    }
    [ObservableProperty]
    public Worker worker;
    [ObservableProperty]
    string displayableNameGet;
    public string DisplayableNameSet {  
        get { return DisplayableNameGet; } 
        set {
            DisplayableNameGet = value;
            Worker.Name = value;
        }
    }
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
    //--------------------------------------------------------------------------
    //Current assignment feature
    ObservableCollection<Assignment> assignments = new();
    public ObservableCollection<Assignment> Assignments
    {
        get
        {
            assignments.Clear();
            foreach (var task in queryService.GetItems<Assignment>().Where(w => w.Workers.Contains(Worker)))
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
    [ObservableProperty]
    public LogicSwitch nameEditMode = new();
    public LogicSwitch DescriptionEditMode = new();

    public void InitializeComponents()
    {
        DisplayablePositionGet = Worker.Position.Name;
        DisplayableNameGet = Worker.Name;
        PossiblePositions = queryService.GetItems<Position>().ToList();
        NextTask = Assignments.Where(a => a.StartTime < DateTime.Now).OrderBy(a => a.StartTime).First();
        CurrentTask = Assignments.Where(w => w.StartTime <= DateTime.Now && w.FinishTime >= DateTime.Now).First();
    }

    public string TaskName
    {
        get => CurrentTask is null ? "None , Next task" : CurrentTask.Name;
    }
    public string SecondaryText
    {
        get => CurrentTask is null ? NextTask.Name : queryService.GetItems<Job>().Where(j => j.Tasks.Contains(CurrentTask)).First().Name;
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
        queryService = st;
        return true;
    }




}
