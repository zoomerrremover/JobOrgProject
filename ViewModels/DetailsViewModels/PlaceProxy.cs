
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.ViewModels.ModelsProxies
{
    [Proxy(ClassLinked = typeof(Place))]
    public partial class PlaceProxy : ThingProxy
    {
        new public Place BindingObject { get; set; }


        // CTORS
        //--------------------------------------------------------------------------------
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is Place)
            {
                var wm = new PlaceProxy(model as Place);
                return wm;
            }
            else return null;
        }
        public PlaceProxy(Place place) : base(place)
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
}
