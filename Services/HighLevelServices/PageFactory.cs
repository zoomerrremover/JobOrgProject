
using Microsoft.Maui.Controls;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.View;

namespace TheJobOrganizationApp.ViewModels;

public class PageFactory
{
    Dictionary<string, DataTemplate> Controls = new();

    ResourceDictionary Resources;

    Converter adaptor;

    public PageFactory(Resources Resources,Converter adaptor)
    {
        this.adaptor = adaptor;
        this.Resources = Resources;
        InitializeTemplates();

    }
    public DetailsPage MakeADetailsPage(Thing Obj)
    {
        var type = Obj.GetType();
        var baseType = typeof(Thing);
        var firstTemplate = Controls.ContainsKey(type.Name) ? type : baseType; 
        var types = new List<Type>
        {
            firstTemplate
        };
        types.AddRange(Obj.GetType().GetInterfaces().ToList());
        List<ContentView> listOfContent = new();
        foreach(Type localType in  types)
        {
            if(Controls.ContainsKey(localType.Name))
            {
                var template = Controls[localType.Name];
                var bindingContext = adaptor.ConvertToViewModel(Obj, localType);
                var content = dataTemplateToContentView(template, bindingContext);
                listOfContent.Add(content);
            }
        }
        return new DetailsPage(listOfContent);
    }

    private ContentView dataTemplateToContentView(DataTemplate template, BaseViewModel bindingContext)
    {
        var localCont = new ContentView
        {
            Content = (Microsoft.Maui.Controls.View)template.CreateContent(),
            BindingContext = bindingContext
        };
        return localCont;
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


