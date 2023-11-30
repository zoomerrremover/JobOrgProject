
using System.Reflection;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class DetailsPageFactory
{
    Dictionary<string, DataTemplate> Controls;

    ResourceDictionary Resources;

    public DetailsPageFactory(ResourceDictionary Resources)
    {
        this.Resources = Resources;
        InitializeTemplates();

    }
    public DetailsPage MakeADetailsPage(Thing Obj)
    {
        var type = Obj.GetType();
        var baseClass = Obj.GetType().BaseType.Name;
        var interfaces = type.GetInterfaces().Select(i=>i.Name).ToList();
        var listOfTemplate = new List<DataTemplate>
        {
            Controls[baseClass]
        };
        Controls.Where(w => interfaces.Contains(w.Key)).ToList().ForEach(w=>listOfTemplate.Add(w.Value));
        DetailsViewModel vm = new(Obj);
        return new DetailsPage(vm, listOfTemplate);
    }

        void InitializeTemplates()
        {
            foreach (var Template in Resources)
            {
                if (Template.Value is DataTemplate || Template.Key.Contains("Details"))
                {
                    Controls[Template.Key.Replace("Details", "")] = Template.Value as DataTemplate;
                }
            }

        }
    }


