
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels;
public class PageFactory: IPageFactory
{
    Dictionary<string, DataTemplate> Controls = new();
    IConverter Convertor;
    public PageFactory(IConverter adaptor,IDataStorage dataStorange,IUserController userController)
    {
        Convertor = adaptor;
        ModelView.queryService = dataStorange;
        ModelView.userController = userController;
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


