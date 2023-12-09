
using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services;

public class SharedControls:BaseViewModel
{
    IDataStorage Data;

    GlobalSettings settings;
    public ObservableCollection<Worker> WorkersPicked { get; set; }


    public SharedControls(IDataStorage storange,GlobalSettings settings)
    {
        Data = storange;
        WorkersPicked = new ObservableCollection<Worker>();
        this.settings = settings;

    }


}


