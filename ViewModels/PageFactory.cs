
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class PageFactory
{
    Dictionary<string, DataTemplate> Controls = new();

    ResourceDictionary Resources;

    ModelToVMAdaptor adaptor;

    public PageFactory(Resources Resources,ModelToVMAdaptor adaptor)
    {
        this.adaptor = adaptor;
        this.Resources = Resources;
        InitializeTemplates();

    }
    public DetailsPage MakeADetailsPage(Thing Obj)
    {
        var type = Obj.GetType();
        var interfaces = type.GetInterfaces().Select(i=>i.Name).ToList();
        var listOfTemplate = new List<DataTemplate>
        {
            Controls[type.Name]
        };
        Controls.Where(w => interfaces.Contains(w.Key)).ToList().ForEach(w=>listOfTemplate.Add(w.Value));
        var bc = adaptor.ConvertToViewModel(Obj);
        return new DetailsPage(bc, listOfTemplate);
    }

        void InitializeTemplates()
        {
            foreach (var Template in Resources)
            {
                if (Template.Value is DataTemplate & Template.Key.Contains("Details"))
                {
                    Controls[Template.Key.Replace("Details", "")] = Template.Value as DataTemplate;
                }
            }

        }
    }


