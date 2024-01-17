
using CommunityToolkit.Mvvm.ComponentModel;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(IHasLocation))]
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
}
