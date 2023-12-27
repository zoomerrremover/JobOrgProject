
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TheJobOrganizationApp.ViewModels
{
    public partial class LogicSwitch:ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Invisible))]
        bool visible = true;
        public bool Invisible { get { return !Visible; } }
        [RelayCommand]
        void SwitchPressed()
        {
            Visible = !Visible;
            if (Visible)
            {
                SwitchOn.Invoke();
            }
            else
            {
                SwitchOff.Invoke();
            }
        }
        public LogicSwitch()
        {
            SwitchOff = () => { };
            SwitchOn= () => { };
        }

        public event Action SwitchOn;

        public event Action SwitchOff;

    }
}
