
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class GSSelector
{
    public Dictionary<string, DataTemplate> TemplatesAndTypes { get; set; } = new();
    static ResourceDictionary Resources { get; set; }

    GlobalSettings GlobalSettings { get; set; }
    void InitializeTemplates()
    {
        foreach (var Template in Resources)
        {
            if (Template.Value is DataTemplate)
            {
                TemplatesAndTypes[Template.Key.Replace("Template","")] = Template.Value as DataTemplate;
            }
        }

    }
    public GSSelector(Resources resource,GlobalSettings settings)
    {
        Resources = resource;
        InitializeTemplates();
    }


}