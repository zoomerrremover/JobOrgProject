
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using TheJobOrganizationApp.Models.Interfaces;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(ITimeBased))]
    public partial class ITimeBasedProxy : ModelView
    {
// CTORS
//----------------------------------------------------------------------------------------------------------
        [ObservableProperty]
        ITimeBased bindingObject;
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is ITimeBased)
            {
                var wm = new ITimeBasedProxy(model as ITimeBased);
                return wm;
            }
            else return null;
        }
        public ITimeBasedProxy(ITimeBased BindingObject)
        {
            this.BindingObject = BindingObject;
            displayableStartDate = BindingObject.StartTime;
        }

        //----------------------------------------------------------------------------------------------------------
        [ObservableProperty]
        DateTime displayableStartDate;
        [ObservableProperty]
        DateTime displayableFinishDate;
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DisplayableStartDate):
                    if (DisplayableStartDate < DisplayableFinishDate)
                    {
                        BindingObject.StartTime = DisplayableStartDate;
                        break;

                    }
                    else
                    {
                        DisplayTimeError.Invoke();
                        displayableStartDate = BindingObject.StartTime;
                        break;
                    }
                    break;
                case nameof(DisplayableFinishDate):
                    if (DisplayableStartDate < DisplayableFinishDate)
                    {
                        BindingObject.FinishTime = DisplayableFinishDate;
                        break;

                    }
                    else
                    {
                        DisplayTimeError.Invoke();
                        displayableFinishDate = BindingObject.FinishTime;
                        break;
                    }
            }
            base.OnPropertyChanged(e);
        }
        public event Action DisplayTimeError = () => { };
    }
}
