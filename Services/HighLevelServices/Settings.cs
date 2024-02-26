using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services.HighLevelServices;

public class Settings : ISettings
{
    public string ServerName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool NotificationPermission { get; set; } = false;
    public bool GeoLocationPermission { get; set; } = false;
    public int LoadPeriodFuture { get; set; } = 7;
    public int LoadPeriodPast { get; set; } = 7;

    static string settingsFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Settings");
    public async Task LoadFromFile()
    {
        string file = Path.Combine(settingsFilePath, "settings.json");
        if (File.Exists(file))
        {
            var settingsSerialized = await File.ReadAllTextAsync(file);
            var Settings = JsonSerializer.Deserialize<Settings>(settingsSerialized);
            ServerName = Settings.ServerName;
            UserName = Settings.UserName;
            Password = Settings.Password;
            NotificationPermission = Settings.NotificationPermission;
            GeoLocationPermission = Settings.GeoLocationPermission;
            LoadPeriodFuture = Settings.LoadPeriodFuture;
            LoadPeriodPast = Settings.LoadPeriodPast;
        }
    }
    public async Task SaveToFile()
    {
        var settingsSerialized = JsonSerializer.Serialize(this);
        if (!Directory.Exists(settingsFilePath))
        {
            Directory.CreateDirectory(settingsFilePath);
        }
        string file = Path.Combine(settingsFilePath, "settings.json");
        await File.WriteAllTextAsync(file, settingsSerialized);
}

}
