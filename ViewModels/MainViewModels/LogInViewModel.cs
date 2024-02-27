
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.View.MainPages;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.MainViewModels;

public partial class LogInViewModel: BaseViewModel
{
    IServerConnectionService serverConnection;

    ISettings settings;

    IErrorService errorService;
    [ObservableProperty]
    string serverName = string.Empty;
    [ObservableProperty]
    string userName = string.Empty;
    [ObservableProperty]
    string password = string.Empty;
    public LogInViewModel(IServerConnectionService serverConnection,IErrorService errorService,ISettings settings)
    {
        this.settings = settings;
        this.serverConnection = serverConnection;
        this.errorService = errorService;
    }

    public async Task tryLoggingIn()
    {
        if (settings.ServerName != string.Empty && settings.UserName != string.Empty && settings.Password != string.Empty)
        {
            if (serverConnection.ServerLogIn(ServerName).Result && serverConnection.ProfileLogIn(settings.UserName, settings.Password).Result)
            {
                await goToApp();
            }

        }
    }

    [RelayCommand]
    async Task logInToServer()
    {
        bool IsConnected = await serverConnection.ServerLogIn(ServerName);
        if(IsConnected)
        {
            await Shell.Current.GoToAsync($"{nameof(LogInPage2)}");
        }
        else
        {
            errorService.CallError("Coundn't log-in to this server.");
        }
    }
    [RelayCommand]
    async Task logInToAccount()
    {
        bool IsConnected = await serverConnection.ProfileLogIn(UserName,Password);
        if (IsConnected)
        {
            settings.ServerName = ServerName;
            settings.UserName = UserName;
            settings.Password = Password;
            await goToApp();
        }
        else
        {
            errorService.CallError("Coundn't log-in to this server.");
        }
    }

    async Task goToApp()
    {
        IsLoading = true;
        serverConnection.StartListening();
        await serverConnection.LoadData();
        await Shell.Current.GoToAsync($"///{nameof(ScheldudePage)}");
        IsLoading = false;
    }
}
