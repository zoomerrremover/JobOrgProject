using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp
{
    public partial class ScheldudePage : ContentPage
    {
        int count = 0;

        public ScheldudePage(ScheldudeViewModel bc)
        {
            BindingContext = bc;
            InitializeComponent();
        }


    }
}