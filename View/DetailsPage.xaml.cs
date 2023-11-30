using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel vm,List<DataTemplate> dataTemplates)
	{
		BindingContext = vm;
        var numberOfElements = dataTemplates.Count;
        var grid = CreateGrid(numberOfElements);
        var i = 0;
        var localCont = new ContentView();
        foreach ( DataTemplate dataTemplate in dataTemplates)
        {
            localCont.BindingContext = vm;
            localCont.Content = (Microsoft.Maui.Controls.View)dataTemplate.CreateContent();
            grid.Children.Add(localCont);
            i++;
        }
        var SV = new ScrollView();
        SV.Content = grid;
        Content = SV;
        InitializeComponent();

    }
    Grid CreateGrid(int rowCount)
    {
        var grid = new Grid();
        rowCount = rowCount > 2 ? 1 + rowCount / 2 : rowCount;
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(450) });
        for (int i = 0; i < rowCount-1; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(450) });
        }
        return grid;
    }
}