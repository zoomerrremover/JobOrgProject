﻿
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class IHasLocationProxy : ModelView
{
    [ObservableProperty]
    IHasLocation bindingObject;
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is IHasLocation)
        {
            var wm = new IHasLocationProxy(model as IHasLocation);
            return wm;
        }
        else return null;
    }
    public IHasLocationProxy(IHasLocation BindingObject)
    {
        this.BindingObject = BindingObject;
    }

}
