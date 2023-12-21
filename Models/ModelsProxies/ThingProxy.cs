
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(Thing))]
    public partial class ThingProxy:ModelView
    {
        [ObservableProperty]
        Thing bindingObject;
        [ObservableProperty]
        LogicSwitch nameEditMode = new();
        [ObservableProperty]
        LogicSwitch descriptionEditMode = new();
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
        string displayableNameGet;
        [ObservableProperty]
        string displayableID;
        public string DisplayableNameSet
        {
            get { return DisplayableNameGet; }
            set
            {
                DisplayableNameGet = value;
                BindingObject.Name = value;
            }
        }
        //--------------------------------------------------------------------------
        [ObservableProperty]
        string displayableDescriptionGet;
        public string DisplayableDescriptionSet
        {
            get { return DisplayableDescriptionGet; }
            set
            {
                DisplayableDescriptionGet = value;
                BindingObject.Description = value;
            }
        }
        //--------------------------------------------------------------------------
        public void InitializeComponents()
        {
            DisplayableNameGet = BindingObject.Name;
            DisplayableDescriptionGet = BindingObject.Description;
        }
    }
}
