

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.Drawing;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;
using Color = Microsoft.Maui.Graphics.Color;

namespace TheJobOrganizationApp.ViewModels;

public partial class ScheldudeViewModel
    : BaseViewModel
{
    IDataStorage Data;

    List<Guid> tasksOnTheScreen = new();

    IPopupNavigation PopUpService;

    WorkerPickerViewModel WorkerPickerVM;
    public ObservableCollection<SchedulerAppointment> appointments { get; }

    ObservableCollection<Assignment> GlobalAssignments;

    void InitializeAppointments (object sender = null,EventArgs e = null)
    {
        appointments.Clear ();
        tasksOnTheScreen.Clear ();
        WorkerPickerVM.WorkersPicked.ToList().ForEach(w =>
        {;
            foreach (var task in GlobalAssignments)
            {
                if (!tasksOnTheScreen.Contains(task.Id))
                {
                    if (task.Workers.Contains(w))
                    {
                        tasksOnTheScreen.Add(task.Id);
                        var newAppointment = new SchedulerAppointment { StartTime = task.StartTime, EndTime = task.FinishTime, Subject = task.Name, Background = w.Color };
                        appointments.Add(newAppointment);
                    }
                }
            }
        });
    }


    public ScheldudeViewModel(WorkerPickerViewModel WorkerPickerVM, IPopupNavigation PopUpService,IAPIService apiservice,IDataStorage storage)
    {
        Data = storage;
        appointments = new();
        this.WorkerPickerVM = WorkerPickerVM;
        this.PopUpService = PopUpService;
        GlobalAssignments= Data.GetItems<Assignment> ();
       // GoToLogInScreen();
        apiservice.Connect();
        apiservice.Initiate();
        GlobalAssignments.CollectionChanged += InitializeAppointments;
        WorkerPickerVM.WorkersPicked.CollectionChanged += InitializeAppointments;
        
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
