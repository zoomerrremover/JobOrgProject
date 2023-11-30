
using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels
{
    public partial class DetailsViewModel:BaseViewModel
    {
        [ObservableProperty]
        Thing objectDisplayed;
        public DetailsViewModel(Thing ObjectDisplayed)
        {
            this.ObjectDisplayed = ObjectDisplayed;
        }
    }
    
}
