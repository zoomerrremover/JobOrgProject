
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
        public ObservableCollection<Worker> WorkersPicked { get; set; }

        public List<string> GSDisplayableModels {  get; set; }

        public SharedControls(IDataStorage storange)
        {
            Data = storange;
            WorkersPicked = new ObservableCollection<Worker>();
        }
    }
}
