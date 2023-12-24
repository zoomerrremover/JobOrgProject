
using System.Reflection;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.Services;

public class GlobalSettings: Iintializable
{
    public List<Type> Models { get; set; } = new();

    public Dictionary<string, string> Settings { get; set; }

    public Dictionary<string, string> Strings { get; set; } = new();
    public Dictionary<string, string> Icons{ get; set; } = new();// Do After API
    public bool InitializeSettings()
    {
        if(InitializeModels())
        {
            return true;
        }
        return false;
    }

    public bool Initialize()
    {
        if (InitializeModels())
        {
            return true;
        }
        return false;
    }
    public bool InitializeModels()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var type in types)
        {
            if (type.IsClass && Attribute.IsDefined(type, typeof(Model)))
            {

                Models.Add(type);
        }

    }
        return true;
    }
}