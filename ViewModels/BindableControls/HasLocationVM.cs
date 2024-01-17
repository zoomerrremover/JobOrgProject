
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasLocationVM : ModelView
{
    [ObservableProperty]
    IHasLocation bindingObject;

    public HasLocationVM(IHasLocation BindingObject)
    {
        this.BindingObject = BindingObject;
    }

}
