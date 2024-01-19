
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

/// <summary>
/// This is a binding model , so you should bind this class to your ui elements.
/// This class is designed to manage a list of objects for display purposes. 
/// It includes built-in functionalities for searching and filtering the list, 
/// as well as adding and editing the objects within it.
/// The class provides optional features such as add and edit buttons, and filters, 
/// which can be incorporated as needed via specific methods.
/// </summary>

public partial class ModelCollectionView : ObservableObject
{
    #region Constructors and main enumerables
    public ObservableCollection<object> DisplayableList { get; set; } = new();
    ObservableCollection<object> elements { get; init; }

    static IDataStorage DataStorage { get; set; }

    /// <summary>
    /// Create new instance of observavle ModelCollectionView . You can add additional
    /// features using builder style methods(etc this.WithAddButtonn(...)
    /// <param name="elements">All the elements it can display.</param>
    /// </summary>
    /// 
    public ModelCollectionView(IDataStorage dataStorange)
    {
        DataStorage = dataStorange;
    }
    public ModelCollectionView(IEnumerable<object> elements)
    {
        var type = elements.First().GetType();
        DataStorage.SubscribeForUpdates(LoadCollection, type);
        this.elements = elements.ToObservableCollection();
        LoadCollection();
    }

    private void LoadCollection(object sender, NotifyCollectionChangedEventArgs e)
    {
        LoadCollection();
    }
    #endregion
    #region AddButton
    public bool AddButtonEnabled { get; private set; } = false;
    public bool AddButtonIsVisible { get; private set; } = false;
    Action AddButtonAction { get; set; }
    /// <summary>
    /// Adds Add button features and properties.
    /// </summary>
    /// <param name="permissionProvider">Bool for a binding user permission to this button.</param>
    /// <param name="addButtonAction">Action when Add button gets pressed.</param>
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
    /// <summary>
    /// Adds Edit button features and properties.
    /// <param name="permissionProvider">Bool for a binding user permission to this button.</param>
    /// <param name="editButtonAction">Action when Edit button gets pressed.</param>
    /// </summary>
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
    /// <summary>
    /// Adds Filter features and properties.
    /// <param name="filters">Filters that will be avaliable in the selection. 
    /// Intakes tuple with the name that will be displayed as a first item and selector as the second.</param>
    /// </summary>
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
        foreach (var item in elements.Where(w => w.ToString().ToLower().Contains(nameLocal)))
        {
            DisplayableList.Add(item);
        }
    }
    #endregion
}
