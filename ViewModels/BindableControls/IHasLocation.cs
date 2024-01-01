
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class IHasLocationProxy : ModelView
{
    [ObservableProperty]
    IHasLocation bindingObject;

    public IHasLocationProxy(IHasLocation BindingObject)
    {
        this.BindingObject = BindingObject;
    }

}
