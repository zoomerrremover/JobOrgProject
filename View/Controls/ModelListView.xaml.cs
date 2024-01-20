namespace TheJobOrganizationApp.View.Controls;

public partial class ModelListView : ContentView
{
    public static readonly BindableProperty ListTitleProperty = BindableProperty.Create(
    nameof(ListTitle), typeof(string), typeof(ModelListView), string.Empty);

    public static readonly BindableProperty ListDataTemplate = BindableProperty.Create(
nameof(DataTemplate), typeof(DataTemplate), typeof(ModelListView), new DataTemplate());
    public DataTemplate DataTemplate
    {
        get => (DataTemplate)GetValue(ListDataTemplate);
        set => SetValue(ListDataTemplate, value);
    }
    public string ListTitle
    {
        get => (string)GetValue(ListTitleProperty);
        set => SetValue(ListTitleProperty, value);
    }
    public ModelListView()
	{
		InitializeComponent();
	}
}