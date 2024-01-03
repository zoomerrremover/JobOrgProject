
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class ModelCollectionView : ObservableObject
{
    #region Constructors and main enumerables
    public ObservableCollection<object> DisplayableList { get; set; } = new();
    ObservableCollection<object> values { get; init; }
    public ModelCollectionView(IEnumerable<object> values)
    {

        this.values = values.ToObservableCollection<object>();
        LoadCollection();
    }
    #endregion
    #region AddButton
    public bool AddButtonEnabled { get; private set; } = false;
    public bool AddButtonIsVisible { get; private set; } = false;
    Action AddButtonAction { get; set; }
    public ModelCollectionView WithAddButton(bool permissionProvider, Action addButtonAction = null)
    {
        AddButtonIsVisible = true;
        AddButtonEnabled = permissionProvider;
        AddButtonAction = addButtonAction;
        return this;
    }
    [RelayCommand]
    void AddButtonPressed()
    {
        if (AddButtonEnabled)
        {
            if (AddButtonAction != null)
            {
                AddButtonAction();
            }
        }
    }
    #endregion
    #region EditButton
    public bool EditButtonEnabled { get; private set; } = false;
    public bool EditButtonIsVisible { get; private set; } = false;
    Action EditButtonAction { get; set; }
    public ModelCollectionView WithEditButton(bool permissionProvider, Action editButtonAction = null)
    {
        EditButtonIsVisible = true;
        EditButtonEnabled = permissionProvider;
        EditButtonAction = editButtonAction;
        return this;
    }
    [RelayCommand]
    void EditButtonPressed()
    {
        if (EditButtonEnabled)
        {
            if (EditButtonAction != null)
            {
                EditButtonAction();
            }
        }
    }
    #endregion
    #region Filters
    public bool FiltersAreVisible { get; private set; } = false;
    public ModelCollectionView WithFilters(params (string, Action<ObservableCollection<object>>)[] filters)
    {
        FiltersAreVisible = true;
        filters.ForEach(w => filterSelectorMethods[w.Item1] = w.Item2);
        filterSelectorMethods.Keys.ForEach(AvaliableFilters.Add);
        selectedString = AvaliableFilters.First().ToString();
        LoadCollection();
        return this;
    }
    void LoadCollection()
    {
        ApplySearchEntry();
        if (FiltersAreVisible)
        {
            ApplyFilters();
        }
    }
    Dictionary<string, Action<ObservableCollection<object>>> filterSelectorMethods = new();
    public ObservableCollection<string> AvaliableFilters { get; set; } = new();
    [ObservableProperty]
    string selectedString = "";
    partial void OnSelectedStringChanged(string value)
    {
        LoadCollection();
    }
    void ApplyFilters()
    {
        var choosedFilter = filterSelectorMethods[SelectedString];
        if (choosedFilter != null)
        {
            choosedFilter.Invoke(DisplayableList);
        }
    }
    #endregion
    #region Search entry
    [ObservableProperty]
    string searchEntry = "";
    partial void OnSearchEntryChanged(string value)
    {
        LoadCollection();
    }
    private void ApplySearchEntry()
    {
        var nameLocal = SearchEntry.ToLower();
        DisplayableList.Clear();
        foreach (var item in values.Where(w => w.ToString().ToLower().Contains(nameLocal)))
        {
            DisplayableList.Add(item);
        }
    }
    #endregion
}
