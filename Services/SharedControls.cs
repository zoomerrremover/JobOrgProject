
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services
{
    public class SharedControls:BaseViewModel
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

        public SharedControls(IDataStorage storange)
        {
            Data = storange;
            WorkersPicked = new ObservableCollection<WorkerUIL>();
        }
    }
}
