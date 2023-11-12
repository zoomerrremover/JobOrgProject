

using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels;

public partial class WorkerPickerViewModel : BaseViewModel
{
    IDataStorage Data;

    GlobalControls contorls;
    public WorkerPickerViewModel(IDataStorage Storange,GlobalControls contorls)
    {
        Data = Storange;
        
        InitiateList();

    }
    void InitiateList()
    {
    ObsWorkers.Clear();
        foreach (var worker in Data.Workers)
        {
        ObsWorkers.Add((WorkerUIL)worker);
        }
    }
    //public void WorkerChoosen(object sender,CheckedChangedEventArgs worker)
    //{
        
    //    if(contorls.WorkersPicked.Contains(worker))
    //    {
    //        throw new Exception("Logic error");
    //    }
    //    contorls.WorkersPicked.Add(worker);
    //}
    public ObservableCollection<WorkerUIL> ObsWorkers { get; } = new();

}
