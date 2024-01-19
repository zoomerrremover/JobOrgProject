
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;

[DetailsViewModel(ClassLinked = typeof(Place))]
public partial class PlaceVM : ThingVM
{
    new public Place BindingObject { get; set; }


    // CTORS
    //--------------------------------------------------------------------------------
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Place)
        {
            var wm = new PlaceVM(model as Place);
            return wm;
        }
        else return null;
    }
    public PlaceVM(Place place) : base(place)
    {
        BindingObject = place;
        Initiate();
    }
    public void Initiate()
    {
        DisplayableAdressGet = BindingObject.Address;
    }

    //--------------------------------------------------------------------------------
    [ObservableProperty]
    string displayableAdressGet;

}
