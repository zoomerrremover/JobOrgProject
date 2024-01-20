namespace TheJobOrganizationApp.View.Controls;

public partial class StringPicker : ContentView
{
    public static readonly BindableProperty PickerTitleProperty = BindableProperty.Create(
   nameof(PickerTitle), typeof(string), typeof(ModelListView), string.Empty);
    public string PickerTitle
    {
        get => (string)GetValue(PickerTitleProperty);
        set => SetValue(PickerTitleProperty, value);
    }
    public StringPicker()
	{
		InitializeComponent();
	}
}