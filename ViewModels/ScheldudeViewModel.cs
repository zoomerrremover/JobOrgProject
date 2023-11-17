

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

    [ObservableProperty]

    GlobalControls controls;
        

    public ScheldudeViewModel(IAPIService apiservice,IDataStorage storage,GlobalControls controls)
    {
        Data = storage;
        this.controls = controls;
       // GoToLogInScreen();
        apiservice.Connect();
        apiservice.Initiate();
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
