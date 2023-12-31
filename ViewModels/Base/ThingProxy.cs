
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.Base
{

    public partial class ThingProxy:ModelView
    {
        [ObservableProperty]
        Thing bindingObject;
        public new static ModelView CreateFromTheModel(Thing model)
        {
                var wm = new ThingProxy(model);
                return wm;
            
        }
        public ThingProxy(Thing BindingObject)
        {
            this.BindingObject = BindingObject;
            InitializeComponents();

        }
        //Editable Name
        //--------------------------------------------------------------------------
        [ObservableProperty]
        string displayableName;
        [ObservableProperty]
        string displayableID;
        [ObservableProperty]
        string displayableDescription;
        //--------------------------------------------------------------------------
        [ObservableProperty]
        bool nameInEditMode = true;
        [ObservableProperty]
        bool descriptionInEditMode = true;

        [RelayCommand]
        protected virtual void NameEditButtonPressed()
        {
            NameInEditMode = !NameInEditMode;
            if(NameInEditMode)
            {
                BindingObject.Name = DisplayableName;
            }
            queryService.TriggerUpdate(BindingObject);
        }
        [RelayCommand]
        protected virtual void DescriptionEditButtonPressed()
        {
            DescriptionInEditMode = !DescriptionInEditMode;
            if (DescriptionInEditMode)
            {
                BindingObject.Description = DisplayableDescription;
            }
            queryService.TriggerUpdate(BindingObject);
        }
        //--------------------------------------------------------------------------
        public void InitializeComponents()
        {
            DisplayableName = BindingObject.Name;
            DisplayableDescription = BindingObject.Description;
            DisplayableID = BindingObject.Id.ToString();
        }
    }
}
