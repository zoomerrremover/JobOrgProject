using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TheJobOrganizationApp.ViewModels;
using System.Linq;

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
            queryService.SubscribeForUpdates(InitializeHolders, typeof(IHasItems));
            InitializeHolders();
            
        }

        private void InitializeHolders(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitializeHolders();
        }

        private void InitializeHolders()
        {
            var localHolders = queryService.GetItemsWithInterface<IHasItems>();
            var values = GetValues(localHolders);
            ModelCollectionView = new(values);
            ModelCollectionView.WithAddButton(false)
                .WithFilters(("Name",NameSelector), ("Quantity",QuantitySelector));
        }

        private ObservableCollection<ThingItemMerge> GetValues(ObservableCollection<Thing> localHolders)
        {
            ObservableCollection<ThingItemMerge> localCol = new();
            foreach (var (holder, item) in from holder in localHolders
                                           where holder is IHasItems
                                           let itemContext = holder as IHasItems
                                           where itemContext.Items.Contains(BindingObject)
                                           let item = itemContext.Items.Where(x => x == BindingObject).FirstOrDefault()
                                           select (holder, item))
            {
                localCol.Add(new ThingItemMerge(holder, item));
            }
            return localCol;
        }

        //--------------------------------------------------------------------------------
        [ObservableProperty]
        string displayablePrice;
        protected override void NameEditButtonPressed()
        {
            if(DataCheckFloat(DisplayablePrice))
            {
                var localPrice = float.Parse(DisplayablePrice);
                BindingObject.Price = localPrice;
            }
            base.NameEditButtonPressed();
        }
        bool DataCheckFloat(string numberToCheck)
        {
            return true; //TODO
        }
        //--------------------------------------------------------------------------------
        ModelCollectionView<ThingItemMerge> ModelCollectionView { get; set; }
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
            var listedItems = holders.ToList()
                                        .OrderByDescending(h => h.thing);
            holders.Clear();
            foreach (var item in listedItems)
            {
                holders.Add(item);
            }
        }
        //--------------------------------------------------------------------------------

    }
}
