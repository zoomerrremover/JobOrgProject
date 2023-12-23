using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;


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
        }
        public ItemProxy(Thing BindingObject) : base(BindingObject)
        {
        }
        public void Initiate()
        {
            filterSelectorMethods["Name"] = NameSelector;
            filterSelectorMethods["Quantity"] = QuantitySelector;
            AvaliableFilters = filterSelectorMethods.Keys.ToList();
            var localHolders = queryService.GetItemsWithInterface<IHasItems>();
            foreach (var holder in localHolders)
            {
                avaliableHolders.Add((holder, holder as IHasItems));
            }
            DisplayHolders();

        }
        //--------------------------------------------------------------------------------
        delegate void ListSelector(ObservableCollection<(Thing, IHasItems)> items);


        [ObservableProperty]
        public ObservableCollection<(Thing, IHasItems)> displayedHolders = new();
        ObservableCollection<(Thing, IHasItems)> avaliableHolders = new();
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
            var filteredList = avaliableHolders.Where(i => i.Item1.Name.ToLower().Contains(SearchPrompt));
            DisplayedHolders.Clear();
            foreach (var item in filteredList)
            {
                DisplayedHolders.Add(item);
            }
        }
        void DisplayHolders()
        {
            DisplayedHolders = avaliableHolders;
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
        public void NameSelector(ObservableCollection<(Thing, IHasItems)> holders)
        {
            var listedItems = holders.ToList()
                .OrderBy(i => i.Item2.Items.Where(w => w == BindingObject)
                    .First().Qty);
            holders.Clear();
            foreach (var item in listedItems)
            {
                holders.Add(item);
            }
        }
        public void QuantitySelector(ObservableCollection<(Thing, IHasItems)> holders)
        {
            var listedItems = holders.ToList().OrderByDescending(h => h.Item1);
            holders.Clear();
            foreach (var item in listedItems)
            {
                holders.Add(item);
            }
        }
        //--------------------------------------------------------------------------------

    }
}
