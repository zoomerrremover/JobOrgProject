
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services;

public class AppSettings:ISettings
{
    public Dictionary<string, object> Settings { get; set; }

    public AppSettings()
    {
        Settings = new Dictionary<string, object>();
    }
}