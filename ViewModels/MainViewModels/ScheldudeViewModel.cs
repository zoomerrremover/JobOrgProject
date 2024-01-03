

using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.PopUpViewModels;
using System.Linq;

namespace TheJobOrganizationApp.ViewModels.MainViewModels;

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
        WorkerPickerVM.WorkersPicked.ForEach(w =>
        {;
            foreach (var task in from task in GlobalAssignments.Where(t => t.Workers.Contains(w))
                                 where !tasksOnTheScreen.Contains(task.Id)
                                 select task)
            {
                tasksOnTheScreen.Add(task.Id);
                var newAppointment = new SchedulerAppointment { StartTime = task.StartTime, EndTime = task.FinishTime, Subject = task.Name, Background = w.Color };
                appointments.Add(newAppointment);
            }
        });
    }

    public ScheldudeViewModel(WorkerPickerViewModel WorkerPickerVM, IPopupNavigation PopUpService,IConnectionService apiservice,IDataStorage storage)
    {
        Data = storage;
        appointments = new();
        this.WorkerPickerVM = WorkerPickerVM;
        this.PopUpService = PopUpService;
        Initialize(WorkerPickerVM);
    }

    private void Initialize(WorkerPickerViewModel WorkerPickerVM)
    {
        GlobalAssignments = Data.GetItems<Assignment>();
        LinkMethods(WorkerPickerVM);
        InitializeAppointments();
    }
    private void LinkMethods(WorkerPickerViewModel WorkerPickerVM)
    {
        GlobalAssignments.CollectionChanged += InitializeAppointments;
        WorkerPickerVM.WorkersPicked.CollectionChanged += InitializeAppointments;
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
