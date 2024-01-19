
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasLocationVM : ModelView
{
    [ObservableProperty]
    IHasLocation bindingObject;
    public bool CanBeEdited { get; set; }

    public HasLocationVM(IHasLocation BindingObject, bool canBeEdited = false)
    {
        this.BindingObject = BindingObject;
        CanBeEdited = canBeEdited;
    }

}
