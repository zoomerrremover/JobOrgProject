using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(Thing bindingContext,List<DataTemplate> dataTemplates)
    {
        var numberOfElements = dataTemplates.Count;
        var grid = CreateGrid(numberOfElements);
        FillGridWithContent(bindingContext, dataTemplates, grid);

        var SV = new ScrollView();
        SV.Content = grid;
        SV.Padding = 10;
        Content = SV;
        InitializeComponent();

    }

    private void FillGridWithContent(Thing bindingContext, List<DataTemplate> dataTemplates, Grid grid)
    {
        var c = 0;
        for (int i = 0; i < dataTemplates.Count; i++)
        {
            var dataTemplate = dataTemplates[i];
            var localCont = new ContentView
            {
                BindingContext = bindingContext,
                Content = (Microsoft.Maui.Controls.View)dataTemplate.CreateContent()
            };
            if(i == 0 || i == dataTemplates.Count - 1)
            {
                Grid.SetColumnSpan(localCont, 2);
                Grid.SetRow(localCont, i);
            }
            else if(i == dataTemplates.Count - 2 || dataTemplates.Count % 2 > 0)
            {
                Grid.SetColumnSpan(localCont, 2);
                Grid.SetRow(localCont, i);
            }
            else
            {
                if (c > 0)
                {
                    Grid.SetRow(localCont, i-1);
                }
                else
                {
                    Grid.SetRow(localCont, i);
                }
                Grid.SetColumn(localCont, c);
            }
            
            grid.Children.Add(localCont);
            c = (c == 1) ? 0 : 1;
        }

    }

    Grid CreateGrid(int rowCount)
    {
        var grid = new Grid();
        rowCount = rowCount > 2 ? (int)rowCount/2 + 1: rowCount;
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(450) });
      
        for (int i = 0; i < rowCount; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Auto) });
            if (i < 2)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

        }
        return grid;
    }
}