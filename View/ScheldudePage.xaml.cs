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

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}