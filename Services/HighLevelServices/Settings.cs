using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.StaticResources;

namespace TheJobOrganizationApp.Services.HighLevelServices;

public class Settings : ISettings
{
    IEncryptingService encryption;
    public string ServerName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool NotificationPermission { get; set; } = false;
    public bool GeoLocationPermission { get; set; } = false;
    public int LoadPeriodFuture { get; set; } = 7;
    public int LoadPeriodPast { get; set; } = 7;

    static string settingsFilePath = AppPaths.settingsFilePath;
    public async Task LoadFromFile()
    {
        string file = Path.Combine(settingsFilePath, "settings.json");
        if (File.Exists(file))
        {
            var settingsSerialized = await File.ReadAllTextAsync(file);
            var Settings = JsonSerializer.Deserialize<Settings>(settingsSerialized);
            ConsumeClone(this,Settings);
            ServerName = encryption.Decrypt(Settings.ServerName);
            UserName = encryption.Decrypt(Settings.UserName);
            Password = encryption.Decrypt(Settings.Password);
        }
    }

    private void ConsumeClone(Settings Consumer,Settings Clonned)
    {
        Consumer.ServerName = Clonned.ServerName;
        Consumer.UserName = Clonned.UserName;
        Consumer.Password = Clonned.Password;
        Consumer.NotificationPermission = Clonned.NotificationPermission;
        Consumer.GeoLocationPermission = Clonned.GeoLocationPermission;
        Consumer.LoadPeriodFuture = Clonned.LoadPeriodFuture;
        Consumer.LoadPeriodPast = Clonned.LoadPeriodPast;
    }

    public Settings(IEncryptingService ecryptionService)
    {
        encryption = ecryptionService;
    }
    public async Task SaveToFile()
    {
        var cloned = new Settings(encryption);
        ConsumeClone(cloned, this);
        cloned.ServerName = encryption.Encrypt(cloned.ServerName);
        cloned.UserName = encryption.Encrypt(cloned.UserName);
        cloned.Password = encryption.Encrypt(cloned.Password);
        var settingsSerialized = JsonSerializer.Serialize(cloned);
        if (!Directory.Exists(settingsFilePath))
        {
            Directory.CreateDirectory(settingsFilePath);
        }
        string file = Path.Combine(settingsFilePath, "settings.json");
        await File.WriteAllTextAsync(file, settingsSerialized);
}

}
