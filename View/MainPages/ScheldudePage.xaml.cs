using TheJobOrganizationApp.ViewModels;
using TheJobOrganizationApp.ViewModels.MainViewModels;

namespace TheJobOrganizationApp;

public partial class ScheldudePage : ContentPage
{

    public ScheldudePage(ScheldudeViewModel bc)
    {
        BindingContext = bc;
        InitializeComponent();

    }


}