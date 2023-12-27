
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace TheJobOrganizationApp.ViewModels
{
    public partial class ModelCollectionView<T>:ObservableObject where T : class
    {
        //--------------------------------------------------------------------------------
        public ObservableCollection<T> DisplayableList { get; set; }
        ObservableCollection<T> values { get; init; }
        public ModelCollectionView(ObservableCollection<T> values)
        {
            this.values = values;
            LoadCollection();
        }
        //--------------------------------------------------------------------------------
        public bool AddButtonEnabled { get; private set; } = false;
        public bool AddButtonIsVisible { get; private set; } = false;
        public void WithAddButton(bool permissionProvider)
        {   
            AddButtonIsVisible = true;
            AddButtonEnabled = permissionProvider;
        }
        //--------------------------------------------------------------------------------
        public bool EditButtonEnabled { get; private set; } = false;
        public bool EditButtonIsVisible { get; private set; } = false;
        public void WithEditButton(bool permissionProvider)
        {
            EditButtonIsVisible = true;
            EditButtonEnabled = permissionProvider;
        }

        //--------------------------------------------------------------------------------
        public bool FiltersAreVisible { get; private set; } = false;
        public void WithFilters(params (string,Action<ObservableCollection<T>>)[] filters)
        {
            FiltersAreVisible = true;
            filters.ForEach(w=>filterSelectorMethods[w.Item1]=w.Item2);
            filterSelectorMethods.Keys.ForEach(AvaliableFilters.Add);
            SelectedString = AvaliableFilters.First();
            LoadCollection();
        }
        void LoadCollection()
        {
            ApplySearchEntry();
            if(FiltersAreVisible)
            {
                ApplyFilters();
            }
        }
        Dictionary<string,Action<ObservableCollection<T>>> filterSelectorMethods = new();
        public ObservableCollection<string> AvaliableFilters { get; set; }
        [ObservableProperty]
        string selectedString;
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
}
