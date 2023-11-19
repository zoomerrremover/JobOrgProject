

using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.Drawing;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;
using Color = Microsoft.Maui.Graphics.Color;

namespace TheJobOrganizationApp.ViewModels;

public partial class ScheldudeViewModel
    : BaseViewModel
{
    IDataStorage Data;

    SharedControls controls;

    List<Guid> tasksOnTheScreen = new();
    public ObservableCollection<SchedulerAppointment> appointments { get; }

    void InitializeAppointments (object sender = null,EventArgs e = null)
    {
        controls.WorkersPicked.ToList().ForEach(w =>
        {
            var tasks = Data.Tasks.Where(t => t.Workers.Contains(w.Worker));
            foreach (var task in tasks)
            {
                if (!tasksOnTheScreen.Contains(task.Id))
                {
                    tasksOnTheScreen.Add(task.Id);
                    var newAppointment = new SchedulerAppointment { StartTime = task.StartTime, EndTime = task.FinishTime, Subject = task.Name, Background = w.Worker.Color  };
                    appointments.Add(newAppointment);
                }
            }
        });
    }

    public ScheldudeViewModel(IAPIService apiservice,IDataStorage storage,SharedControls controls)
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
