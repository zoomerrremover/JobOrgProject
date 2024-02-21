
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.MainViewModels;

public partial class SettingsViewModel:BaseViewModel
{
    IEmainAndMobileHandler emaiAndMobileHandler;

    ISettings settings;

    IUserController userController;

    [ObservableProperty]
    string email;
    [RelayCommand]
    async Task ChangeEmail()
    {
        if (!emaiAndMobileHandler.ChangeEmail(Email))
        {
            Email = userController.User.EmailForLogIn;
            return;
        }
       settings.UserName = Email;
    }
    [ObservableProperty]
    int loadPerPast;
    [ObservableProperty]
    int loadPerFuture;
    public List<int> loadPerPickerContent { get => Enumerable.Range(1, 30).ToList(); }
    [RelayCommand]
    void SaveButton()
    {
        settings.LoadPeriodFuture = LoadPerFuture;
        settings.LoadPeriodPast = LoadPerPast;
    }
    public async Task OnLeaving()
    {
        await Task.Run(settings.SaveToFile);
    }
    [ObservableProperty]
    bool notificationPermission;

    [ObservableProperty]
    bool geoLocationPermission;

    partial void OnNotificationPermissionChanged(bool value)
    {
       settings.NotificationPermission = value;
    }
    partial void OnGeoLocationPermissionChanged(bool value)
    {
        settings.GeoLocationPermission = value;
    }

    public SettingsViewModel(IUserController userController,ISettings settings,IEmainAndMobileHandler emaiAndMobileHandler)
    {
        this.userController = userController;
        this.settings = settings;
        this.emaiAndMobileHandler = emaiAndMobileHandler;
    }
}
