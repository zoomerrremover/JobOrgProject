
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.MainViewModels;

public partial class LoadingPageViewModels: BaseViewModel
{
    [ObservableProperty]
    double progress = 0;
    IResources resources;
    ISettings settings;
    IServerConnectionService connectionService;
    LogInPage logInPage;
    public LoadingPageViewModels(LogInPage logInPAge,IResources resources, ISettings settings, IServerConnectionService serverConnection)
    {
       logInPage = logInPAge;
       this.resources = resources;
       this.settings = settings;
       this.connectionService = serverConnection;
    }
    public async Task StartLoading()
    {
        IsLoading = true;
        await connectionService.ConnectAsync();
        Progress += 0.2;
        string version = AppInfo.Current.VersionString;
        await connectionService.GetAssembly(version);
        Progress += 0.2;
        await resources.LoadContent();
        Progress += 0.2;
        await settings.LoadFromFile();
        Progress = 100;
        await Shell.Current.Navigation.PushAsync(logInPage);
        IsLoading = false;

    }
}
