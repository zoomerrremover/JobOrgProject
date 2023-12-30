
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.ViewModels;

public partial class ModelCollectionView:ObservableObject
{
    //--------------------------------------------------------------------------------
    public ObservableCollection<object> DisplayableList { get; set; } = new();
    ObservableCollection<object> values { get; init; }
    public ModelCollectionView(IEnumerable<object> values)
    {
        
        this.values = values.ToObservableCollection<object>();
        LoadCollection();
    }
    //--------------------------------------------------------------------------------
    public bool AddButtonEnabled { get; private set; } = false;
    public bool AddButtonIsVisible { get; private set; } = false;
    public ModelCollectionView WithAddButton(bool permissionProvider)
    {   
        AddButtonIsVisible = true;
        AddButtonEnabled = permissionProvider;
        return this;
    }
    //--------------------------------------------------------------------------------
    public bool EditButtonEnabled { get; private set; } = false;
    public bool EditButtonIsVisible { get; private set; } = false;
    public ModelCollectionView WithEditButton(bool permissionProvider)
    {
        EditButtonIsVisible = true;
        EditButtonEnabled = permissionProvider;
        return this;
    }

    //--------------------------------------------------------------------------------
    public bool FiltersAreVisible { get; private set; } = false;
    public ModelCollectionView WithFilters(params (string,Action<ObservableCollection<object>>)[] filters)
    {
        FiltersAreVisible = true;
        filters.ForEach(w=>filterSelectorMethods[w.Item1]=w.Item2);
        filterSelectorMethods.Keys.ForEach(AvaliableFilters.Add);
        selectedString = AvaliableFilters.First().ToString();
        LoadCollection();
        return this;
    }
    void LoadCollection()
    {
        ApplySearchEntry();
        if(FiltersAreVisible)
        {
            ApplyFilters();
        }
    }
        Dictionary<string,Action<ObservableCollection<object>>> filterSelectorMethods = new();
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
    //--------------------------------------------------------------------------------
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
}
