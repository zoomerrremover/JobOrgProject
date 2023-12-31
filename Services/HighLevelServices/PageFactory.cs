
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
namespace TheJobOrganizationApp.ViewModels;
public class PageFactory: IPageFactory
{
    Dictionary<string, DataTemplate> Controls = new();
    IConverter Convertor;
    public PageFactory(IConverter adaptor)
    {
        this.Convertor = adaptor;
    }

    public ContentPage MakeACreatePage(Type type)
    {
        throw new NotImplementedException();
    }

    public ContentPage MakePage(Thing model)
    {
        var ViewModel = Convertor.ConvertToViewModel(model);
        var Page = Convertor.ConvertToContentPage(model);
        Page.BindingContext =ViewModel;
        return Page;

    }
}


