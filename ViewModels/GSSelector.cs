
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class GSSelector
{
    public Dictionary<string, DataTemplate> TemplatesAndTypes { get; set; } = new();
    static ResourceDictionary Resources { get; set; }

    void InitializeTemplates()
    {
        foreach (var Template in Resources)
        {
            if (Template.Value is DataTemplate || Template.Key.Contains("Template"))
            {
                TemplatesAndTypes[Template.Key.Replace("Template","")] = Template.Value as DataTemplate;
            }
        }
    }
    public DataTemplate ChooseTemplate(string key)
    {
        if (TemplatesAndTypes.ContainsKey(key))
        {
            return TemplatesAndTypes[key];
        }
        else { return TemplatesAndTypes["Thing"]; }
    }
    public GSSelector(Resources resource)
    {
        Resources = resource;
        InitializeTemplates();
    }


}   