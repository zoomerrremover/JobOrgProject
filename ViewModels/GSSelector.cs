
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class GSSelector:DataTemplateSelector
{
    Dictionary<string, DataTemplate> TemplatesAndTypes { get; set; } = new();
    public string ModelSelected {  get; set; }
    static ResourceDictionary Resources { get; set; }
    public void InitializeTemplates()
    {
        foreach (var Template in Resources)
        {
            if (Template.Value is DataTemplate)
            {
                TemplatesAndTypes[Template.Key] = Template.Value as DataTemplate;
            }
        }

    }
    public GSSelector(Resources resource)
    {
        Resources = resource;
        InitializeTemplates();
    }
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        DataTemplate template;
        try
        {
            template = TemplatesAndTypes[$"{ModelSelected}Template"];
        }
        catch
        {
            template = null;
        }

           return template;
        

    }
}