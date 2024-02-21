
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.MainViewModels;

public partial class SettingsViewModel:BaseViewModel
{
    IEmainAndMobileHandler emaiAndMobileHandler;

    ISettings settings;

    IUserController userController;

    IVereficationService vereficationService;

    [ObservableProperty]
    string email;
    [RelayCommand]
    async Task ChangeEmail()
    {
        var result = await emaiAndMobileHandler.ChangeEmail(Email);
        if (!result)
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
    [RelayCommand]
    async Task OnLogOut()
    {
        await vereficationService.LogOut();
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
    
    public SettingsViewModel(IVereficationService vereficationService,IDataStorage dataStorange,IUserController userController,ISettings settings,IEmainAndMobileHandler emaiAndMobileHandler)
    {
        this.vereficationService = vereficationService;
        this.userController = userController;
        this.settings = settings;
        this.emaiAndMobileHandler = emaiAndMobileHandler;
    }
    public void LoadContent()
    {
        Email = userController.User.EmailForLogIn;
        NotificationPermission = settings.NotificationPermission;
        GeoLocationPermission = settings.GeoLocationPermission;
        LoadPerPast = settings.LoadPeriodPast;
        LoadPerFuture = settings.LoadPeriodFuture;
    }
}
