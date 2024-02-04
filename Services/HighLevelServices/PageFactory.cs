
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels;
public class PageFactory: IPageFactory
{
    Dictionary<string, DataTemplate> Controls = new();
    IConverter Convertor;
    public PageFactory(IReflectionContent refCont,IErrorService errorService,IConverter adaptor,IDataStorage dataStorange,IUserController userController)
    {
        Convertor = adaptor;
        ModelView.SetStaticFields(refCont,errorService,dataStorange, userController,this);
    }

    public ContentPage MakeACreatePage(Type type)
    {
        throw new NotImplementedException();
    }

    public ContentPage MakeACreatePage(Type type, Thing PreSets = null)
    {
        throw new NotImplementedException();
    }

    public ContentPage MakeADetailsPage(Thing model)
    {
        var Page = Convertor.ConvertToContentPage(model);
        var ViewModel = Convertor.ConvertToViewModel(model);
        Page.BindingContext =ViewModel;
        return Page;

    }
}


