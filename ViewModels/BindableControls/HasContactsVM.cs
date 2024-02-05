
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasContactsVM
    : ModelView
{
    IHasContacts bindingObject;
    public HasContactsVM(IHasContacts bindingObject)
    {
        this.bindingObject = bindingObject;
    }

    public string Cell
    {
        get => bindingObject.PhoneNumber;
        set => bindingObject.PhoneNumber = value;
    }
    public string Buisiness
    {
        get => bindingObject.Buissness;
        set => bindingObject.Buissness = value;
    }
    public string Email
    {
        get => bindingObject.Email;
        set => bindingObject.Email = value;
    }

}
