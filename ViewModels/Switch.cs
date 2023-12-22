
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
            OnSwitchPressed.Invoke();
        }
        public LogicSwitch()
        {
            OnSwitchPressed = () => { };
        }

        public event Action OnSwitchPressed;

    }
}
