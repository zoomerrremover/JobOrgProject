
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;

[DetailsViewModel(ClassLinked = typeof(Place))]
public partial class PlaceVM : ThingVM
{
    #region CTOR
    new public Place BindingObject { get; set; }
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
    public override void LoadContent()
    {
        base.LoadContent();
        HasItemsVM.Initialize();
    }
    public void Initiate()
    {
        InitiateLocationVM();
        InitializeHasItemsVM();
    }
    #endregion
    #region Location
    void InitiateLocationVM()
    {
        HasLocationVM = new(BindingObject, true);
    }
    public HasLocationVM HasLocationVM { get; set; }
    #endregion
    #region Items
    void InitializeHasItemsVM()
    {
        HasItemsVM = new(BindingObject);
    }
    public HasItemsVM HasItemsVM { get; set; }
    #endregion
    #region Jobs and AssignmentsHere 
    //To Be Added , The feature should include list of Jobs and assignments inside them 
    #endregion
}
