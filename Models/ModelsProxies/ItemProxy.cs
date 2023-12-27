using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(Item))]
    public partial class ItemProxy : ThingProxy
    {
        new public Item BindingObject { get; set; }


        // CTORS
        //--------------------------------------------------------------------------------
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is Item)
            {
                var wm = new ItemProxy(model as Item);
                return wm;
            }
            else return null;
        }
        public ItemProxy(Item item) : base(item)
        {
            BindingObject = item;
            Initialize();
        }
        public ItemProxy(Thing BindingObject) : base(BindingObject)
        {
        }
        public void Initialize()
        {
            InitializeFilters();
            queryService.SubscribeForUpdates(InitializeHolders, typeof(IHasItems));
            InitializeHolders();
            DisplayHolders();
        }

        private void InitializeHolders(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitializeHolders();
        }

        private void InitializeFilters()
        {
            filterSelectorMethods["Name"] = NameSelector;
            filterSelectorMethods["Quantity"] = QuantitySelector;
            AvaliableFilters = filterSelectorMethods.Keys.ToList();
        }

        private void InitializeHolders()
        {
            var localHolders = queryService.GetItemsWithInterface<IHasItems>();
            foreach (var holder in localHolders)
            {
                if (holder is IHasItems)
                {
                    var itemContext = holder as IHasItems;
                    if (itemContext.Items.Contains(BindingObject))
                    {
                        var item = itemContext.Items.Where(x => x == BindingObject).FirstOrDefault();
                        avaliableHolders.Add(new ThingItemMerge(holder, item));
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        delegate void ListSelector(ObservableCollection<ThingItemMerge> items);
        [ObservableProperty]
        public ObservableCollection<ThingItemMerge> displayedHolders = new();
        ObservableCollection<ThingItemMerge> avaliableHolders = new();
        string searchPrompt = "";
        public string SearchPrompt
        {
            get => searchPrompt.ToLower();

            set
            {
                Task.Delay(250);
                searchPrompt = value;
                DisplayHolders();

            }
        }
        void ApplySearchQuery()
        {
            var filteredList = avaliableHolders.Where(i => i.thing.Name.ToLower().Contains(SearchPrompt));
            DisplayedHolders.Clear();
            foreach (var item in filteredList)
            {
                DisplayedHolders.Add(item);
            }
        }
        void DisplayHolders()
        {
            ApplySearchQuery();
            ApplyFilters();
        }
        // Choosable filters feature
        //--------------------------------------------------------------------------------
        Dictionary<string, ListSelector> filterSelectorMethods = new();
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
                DisplayHolders();
            }
        }
        void ApplyFilters()
        {
            var choosedFilter = AvaliableFilters[FilterChoice];
            var filteringMethod = filterSelectorMethods[choosedFilter];
            if (filteringMethod != null)
            {
                filteringMethod.Invoke(DisplayedHolders);
            }
        }

        // Filters
        //--------------------------------------------------------------------------------
        public void NameSelector(ObservableCollection<ThingItemMerge> holders)
        {
            var listedItems = holders.ToList()
                .OrderBy(i => i.item.Qty);
            holders.Clear();
            foreach (var item in listedItems)
            {
                holders.Add(item);
            }
        }
        public void QuantitySelector(ObservableCollection<ThingItemMerge> holders)
        {
            var listedItems = holders.ToList().OrderByDescending(h => h.thing);
            holders.Clear();
            foreach (var item in listedItems)
            {
                holders.Add(item);
            }
        }
        //--------------------------------------------------------------------------------

    }
}
