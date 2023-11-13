

using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public partial class ScheldudeViewModel
    : BaseViewModel
{
    GlobalControls controls;

    IDataStorage storage;

    [ObservableProperty]
    ObservableCollection<SchedulerAppointment> appointments = new();

    public ScheldudeViewModel(IAPIService apiservice,GlobalControls controls,IDataStorage storage)
    {
        this.controls = controls;
        this.storage = storage;
        GoToLogInScreen();
        apiservice.Connect();
        apiservice.Initiate();
        InitiateAppointment();
    }
    public void InitiateAppointment()
    {
        var ListofIds = controls.WorkersPicked.Select(w=>w.Worker.Id).ToList();
        foreach (var pickedWorkerID in ListofIds)
        {
            foreach (var task in storage.Tasks)
            {
                if(ListofIds.Contains(pickedWorkerID))
                {
                    var NewAppointment = new SchedulerAppointment { StartTime = task.StartTime , EndTime = task.FinishTime , Subject = task.Name };
                    Appointments.Add(NewAppointment);
                }
            }
        }
    }
    async Task GoToLogInScreen()
    {

        await Shell.Current.GoToAsync($"{nameof(LogInPage)}");
    }
    public ScheldudeViewModel()
    {
          GoToLogInScreen();
    }
}
