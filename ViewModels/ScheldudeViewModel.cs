

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
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

    IPopupNavigation PopUpService;

    WorkerPickerViewModel WorkerPickerVM;
    public ObservableCollection<SchedulerAppointment> appointments { get; }

    void InitializeAppointments (object sender = null,EventArgs e = null)
    {
        appointments.Clear ();
        tasksOnTheScreen.Clear ();
        controls.WorkersPicked.ToList().ForEach(w =>
        {
            var tasks = Data.Assignments.Where(t => t.Workers.Contains(w));
            foreach (var task in tasks)
            {
                if (!tasksOnTheScreen.Contains(task.Id))
                {
                    tasksOnTheScreen.Add(task.Id);
                    var newAppointment = new SchedulerAppointment { StartTime = task.StartTime, EndTime = task.FinishTime, Subject = task.Name, Background = w.Color  };
                    appointments.Add(newAppointment);
                }
            }
        });
    }

    public ScheldudeViewModel(Initializator init, WorkerPickerViewModel WorkerPickerVM, IPopupNavigation PopUpService,IAPIService apiservice,IDataStorage storage,SharedControls controls)
    {
        Data = storage;
        this.controls = controls;
        appointments = new();
        this.WorkerPickerVM = WorkerPickerVM;
        this.PopUpService = PopUpService;
        controls.WorkersPicked.CollectionChanged += InitializeAppointments;
       // GoToLogInScreen();
        apiservice.Connect();
        apiservice.Initiate();
        InitializeAppointments();
    }
    [RelayCommand]
    void WorkerPicker()
    {

        PopUpService.PushAsync(new WorkerPickerPage(WorkerPickerVM));
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
