using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;
using TheJobOrganizationApp.ViewModels.ModelWrappers;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;

[DetailsViewModel(ClassLinked = typeof(Item))]
public partial class ItemVM : ThingVM
{
    #region Ctors
    new public Item BindingObject { get; set; }
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Item)
        {
            var wm = new ItemVM(model as Item);
            return wm;
        }
        else return null;
    }
    public ItemVM(Item item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }
    public void Initialize()
    {
        HoldersCollectionView = new ModelCollectionView()
                                    .WithFilters(("Name", NameSelector), ("Quantity", QuantitySelector));
        DisplayablePrice = BindingObject.Price.ToString();
        
    }
    public override void LoadContent() {
        base.LoadContent();
        InitializeHolders();
    }

    #endregion
    #region Holders
    public ModelCollectionView HoldersCollectionView { get; set; }
    private void InitializeHolders()
    {
        var localHolders = dataStorage.GetItemsWithInterface<IHasItems>();
        var values = GetHoldersWithItemQTY(localHolders);
        HoldersCollectionView.Initiate(values);
    }
    private List<ThingItemMerge> GetHoldersWithItemQTY(IEnumerable<Thing> localHolders)
    {
        List<ThingItemMerge> localCol = new();
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
    #endregion
    #region Price
    [ObservableProperty]
    string displayablePrice;
    [ObservableProperty]
    bool priceInEditMode = true;
    [RelayCommand]
    public void PriceEditButtonPressed()
    {
        PriceInEditMode = !PriceInEditMode;
        float localPrice;
        float.TryParse(DisplayablePrice, out localPrice);
        if (EditPermission && PriceInEditMode && BindingObject.Price != localPrice)
        {
            CreateChangeHistoryRecord("Price", BindingObject.Price.ToString(), localPrice.ToString());
            BindingObject.Price = localPrice;
        }
    }
    #endregion
    #region Selectors for HolderView
    public void NameSelector(ObservableCollection<object> holders)
    {
        var newHolders = holders.OfType<ThingItemMerge>();
        newHolders.OrderBy(t => t.thing).OfType<object>();
        holders = newHolders.ToObservableCollection<object>();
        
    }
    public void QuantitySelector(ObservableCollection<object> holders)
    {
        holders = holders.OfType<ThingItemMerge>() // Cast objects to ThingMerged
                     .OrderByDescending(h => h.item.Qty) // Sort ThingMerged objects
                     .ToObservableCollection<object>();
    }
    #endregion

}
