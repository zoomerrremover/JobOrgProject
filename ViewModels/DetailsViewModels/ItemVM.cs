using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        InitializeHolders();
        
    }


    private void InitializeHolders()
    {
        var localHolders = dataStorage.GetItemsWithInterface<IHasItems>();
        var values = GetHoldersWithItemQTY(localHolders);
        ModelCollectionView = new(values.OfType<object>());
        ModelCollectionView.WithAddButton(false)
            .WithFilters(("Name", NameSelector), ("Quantity", QuantitySelector));
    }
    #endregion
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
    public ModelCollectionView ModelCollectionView { get; set; }
    // Filters
    //--------------------------------------------------------------------------------
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
    //--------------------------------------------------------------------------------

}
