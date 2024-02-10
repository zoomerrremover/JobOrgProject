
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels;
public class PageFactory: IPageFactory
{
    Dictionary<string, DataTemplate> Controls = new();
    IConverter Convertor;
    IUserController UserController;
    IDataStorage dataStorage;
    public PageFactory(IReflectionContent refCont,IErrorService errorService,IConverter adaptor,IDataStorage dataStorange,IUserController userController)
    {
        Convertor = adaptor;
        this.dataStorage = dataStorange;
        UserController = userController;
        ModelView.SetStaticFields(refCont,errorService,dataStorange, userController,this);
    }


    public ContentPage MakeACreatePage(Type type, Thing PreSets = null)
    {
        Thing template = (Thing)Activator.CreateInstance(type);
        if(PreSets is not null)
        {
            template = PreSets;
        }
        UserController.CreateHistoryRecord(template, Models.Misc.HistoryActionType.Added);
        dataStorage.AddThing(template);
        return MakeADetailsPage(template);
        
    }

    public ContentPage MakeADetailsPage(Thing model)
    {
        var Page = Convertor.ConvertToContentPage(model);
        var ViewModel = Convertor.ConvertToViewModel(model);
        Page.BindingContext =ViewModel;
        return Page;

    }
}


