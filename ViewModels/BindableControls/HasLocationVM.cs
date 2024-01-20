
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

[Obsolete("Undone don't use it for now.")]
public partial class HasLocationVM : ModelView
{
    new IHasLocation BindingObject;
    public bool CanBeEdited { get; set; }
    // Needs to be done with Google maps API , I don't wanna go into API's and stuff for now
    // Deactevated until done
    public HasLocationVM(IHasLocation BindingObject, bool canBeEdited = false)
    {
        this.BindingObject = BindingObject;
        CanBeEdited = canBeEdited;
    }

}
