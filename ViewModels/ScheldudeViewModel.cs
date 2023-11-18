

using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public partial class ScheldudeViewModel
    : BaseViewModel
{
    IDataStorage Data;

    GlobalControls controls;

    public ObservableCollection<SchedulerAppointment> appointments { get; }

    void InitializeAppointments (object sender = null,EventArgs e = null)
    {
        controls.WorkersPicked.ToList().ForEach(w =>
        {
            var tasks = Data.Tasks.Where(t => t.Workers.Contains(w.Worker));
            foreach (var task in tasks)
            {
                if (!controls.tasksOnTheScreen.Contains(task.Id))
                {
                    controls.tasksOnTheScreen.Add(task.Id);
                    var newAppointment = new SchedulerAppointment { StartTime = task.StartTime, EndTime = task.FinishTime, Subject = task.Name , Background = w.Worker.Color};
                    appointments.Add(newAppointment);
                }
            }
        });
    }

    public ScheldudeViewModel(IAPIService apiservice,IDataStorage storage,GlobalControls controls)
    {
        Data = storage;
        this.controls = controls;
        appointments = new();
        controls.WorkersPicked.CollectionChanged += InitializeAppointments;
       // GoToLogInScreen();
        apiservice.Connect();
        apiservice.Initiate();
        InitializeAppointments();
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
