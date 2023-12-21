
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(IHasItems))]
    public partial class IHasItemsProxy : ModelView
    {
        [ObservableProperty]
        IHasItems bindingObject;
        [ObservableProperty]
        public ObservableCollection<Item> displayedItems = new();
        [ObservableProperty]
        List<string> avaliableFilters = new();

        int filterChoice = 0;
        public int FilterChoice
        {
            get => filterChoice;
            set
            {
                Task.Delay(250);
                filterChoice = value;
                ApplySearchQuery();
                ApplyFilters();
            }
        }
        string searchPrompt = "";
        public string SearchPrompt
        {
            get => searchPrompt.ToLower();

            set
            {
                Task.Delay(250);
                searchPrompt = value;
                ApplySearchQuery();
                ApplyFilters();

            }
        }
        public void ApplyFilters()
        {
            var choosedFilter = AvaliableFilters[FilterChoice];
            var filteringMethod = filterSelectorMethods[choosedFilter];
            if (filteringMethod != null)
            {
                filteringMethod.Invoke(DisplayedItems);
            }
        }
        public void ApplySearchQuery()
        {
            var filteredList = BindingObject.Items.Where(i => i.Name.ToLower().Contains(SearchPrompt));
            DisplayedItems.Clear();
            foreach (var item in filteredList)
            {
                DisplayedItems.Add(item);
            }
        }
        [ObservableProperty]

        LogicSwitch rolloutButtonSwitch = new();

        [ObservableProperty]
        int screenHeight = 100;

        void ExtendScreen()
        {
            if(RolloutButtonSwitch.Invisible)
            {

                    Task.Delay(100);
                    ScreenHeight = 400;
            }
            else
            {
                for (int i = 0; i < 350; i++)
                {
                    Task.Delay(50);
                    ScreenHeight =70;
                }
            }
        }
        Dictionary<string, ListSelector> filterSelectorMethods = new();
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is IHasItems)
            {
                var wm = new IHasItemsProxy(model as IHasItems);
                return wm;
            }
            else return null;
        }
        public IHasItemsProxy(IHasItems BindingObject)
        {
            this.BindingObject = BindingObject;
            Initiate();
        }
        public void Initiate()
        {
            RolloutButtonSwitch.OnSwitchPressed += ExtendScreen;
            filterSelectorMethods["Name"] = NameSelector;
            filterSelectorMethods["Quantity"] = QuantitySelector;
            AvaliableFilters = filterSelectorMethods.Keys.ToList();
        }
        public void NameSelector(ObservableCollection<Item> items)
        {
            var listedItems = items.ToList().OrderBy(i=>i.Name);
            items.Clear();
            foreach (var item in listedItems)
            {
                items.Add(item);
            }
        }
        public void QuantitySelector(ObservableCollection<Item> items)
        {
            var listedItems = items.ToList().OrderByDescending(i => i.Qty);
            items.Clear();
            foreach (var item in listedItems)
            {
                items.Add(item);
            }
        }
        delegate void ListSelector(ObservableCollection<Item> items);

    }
}
