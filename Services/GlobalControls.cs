
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services
{
    public class GlobalControls:BaseViewModel
    {
        IDataStorage Data;
        public ObservableCollection<WorkerUIL> WorkersPicked { get; set; }
        //    get
        //    {
        //        var localReturn = new ObservableCollection<WorkerUIL>();
        //        Data.Workers.Where(w => w.IsPicked == true).ToList().ForEach(w => localReturn.Add(w));
        //        return localReturn;
        //    }
        //}
        public ObservableCollection<SchedulerAppointment> appointments { get; set; }

        public List <Guid> tasksOnTheScreen = new();
        public GlobalControls(IDataStorage storange)
        {
            Data = storange;
            appointments = new ObservableCollection<SchedulerAppointment>();
            WorkersPicked = new ObservableCollection<WorkerUIL>();
        }
    }
}
