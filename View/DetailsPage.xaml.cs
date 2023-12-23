using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.ModelsProxies;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(List<ContentView> content)
    {
        var numberOfElements = content.Count;
        var grid = CreateGrid(numberOfElements);
        FillGridWithContent(content, grid);

        var SV = new ScrollView();
        SV.Content = grid;
        SV.Padding = 10;
        Content = SV;
        InitializeComponent();

    }

    private void FillGridWithContent(List<ContentView> content, Grid grid)
    {
        var c = 0;
        for (int i = 0; i < content.Count; i++)
        {

            if(i == 0 || i == content.Count - 1)
            {
                Grid.SetColumnSpan(content[i], 2);
                Grid.SetRow(content[i], i);
            }
            else if(i == content.Count - 2 || content.Count % 2 > 0)
            {
                Grid.SetColumnSpan(content[i], 2);
                Grid.SetRow(content[i], i);
            }
            else
            {
                if (c > 0)
                {
                    Grid.SetRow(content[i], i-1);
                }
                else
                {
                    Grid.SetRow(content[i], i);
                }
                Grid.SetColumn(content[i], c);
            }
            
            grid.Children.Add(content[i]);
            c = (c == 1) ? 0 : 1;
        }

    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.Navigation.RemovePage(this);
        Shell.Current.GoToAsync($"{nameof(GlobalSearchPage)}");
        return true;
    }
    Grid CreateGrid(int rowCount)
    {
        var grid = new Grid();
        rowCount = rowCount > 2 ? (int)rowCount/2 + 1: rowCount;
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
      
        for (int i = 0; i < rowCount; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Auto) });
            if (i < 2)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

        }
        grid.Padding = new Thickness(35, 0, 35, 0);
        grid.RowSpacing = 35;
        return grid;
    }
}