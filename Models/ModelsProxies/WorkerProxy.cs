
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Models.ModelsProxies;
[ObservableObject]
public partial class WorkerProxy:Worker
{
    static IDataStorage queryService;

    ObservableCollection<Assignment> assignments;
    public ObservableCollection<Assignment> Assignments
    {
        get
        {
            assignments.Clear();
            foreach (var task in queryService.GetItems<Assignment>().Where(w => w.Workers.Contains(this)))
            {
                assignments.Add(task);
            }
            return assignments;

        }
    }

    //Current assignment feature
    [ObservableProperty]
    Assignment currentTask;
    [ObservableProperty]
    Assignment nextTask;
    public WorkerProxy()
    {
        nextTask = Assignments.Where(a => a.StartTime > DateTime.Now).OrderBy(a => a.StartTime).FirstOrDefault();
        currentTask = Assignments.Where(w => w.StartTime <= DateTime.Now && w.FinishTime >= DateTime.Now).First();
    }
    public string TaskName
    {
        get => CurrentTask is null ? "None , Next task" : CurrentTask.Name;
    }
    public bool IsCurrentTaskActive { get => CurrentTask is null ? false : true; }

    public string BStartTime { get {
                                var task = CurrentTask is null ? NextTask.StartTime : CurrentTask.StartTime;
                                return task.ToString("d"); }
                              }
    public string BFinishTime 
    {
        get
        {
            var task = CurrentTask is null ? NextTask.FinishTime : CurrentTask.FinishTime;
            return task.ToString("d");
        }
    }
    public static bool Initialize(IDataStorage st)
    {
        queryService = st;
        return true;
    }




}
