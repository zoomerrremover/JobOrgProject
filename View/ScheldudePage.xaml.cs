using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp;

public partial class ScheldudePage : ContentPage
{

    public ScheldudePage(ScheldudeViewModel bc)
    {
        BindingContext = bc;
        InitializeComponent();

    }


}