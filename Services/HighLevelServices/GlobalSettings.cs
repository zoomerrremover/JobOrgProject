
namespace TheJobOrganizationApp.Services;

public class GlobalSettings
{
    public Dictionary<string, string> Settings { get; set; }
    public Dictionary<string, string> Strings { get; set; } = new();
    public Dictionary<string, string> Icons{ get; set; } = new();// Do After API
    public bool InitializeSettings()
    {
        return false;
    }
    public bool Initialize()
    {
        return true;

    }
}