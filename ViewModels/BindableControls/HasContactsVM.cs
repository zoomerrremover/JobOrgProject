
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasContactsVM
    : ModelView
{
    [ObservableProperty]
    IHasContacts bindingObject;
    public HasContactsVM(IHasContacts bindingObject)
    {

        this.bindingObject = bindingObject;

    }

}
