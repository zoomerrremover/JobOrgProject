
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasItemsVM : ModelView
{
    #region CTORS
    new IHasItems BindingObject;
    public HasItemsVM(IHasItems BindingObject)
    {
        this.BindingObject = BindingObject;
        var permission = userController.GetPermission(typeof(Assignment), RuleType.Create);
        ItemsCollectionView = new ModelCollectionView()
                                       .WithAddButton(permission, AddButton)
                                       .WithFilters(("Name", NameSelector),
                                                    ("Quantity", QuantitySelector));
        SetPermissions();
    }
    //public void LoadAsync()
    //{
    //    ItemsCollectionView.LoadCollection();
    //}
    #endregion
    #region Items
    public ModelCollectionView ItemsCollectionView { get; set; }

    public void Initialize()
    {
        ItemsCollectionView.Initiate(BindingObject.Items);
    }

    void AddButton()
    {
        //TODO : Create a pop out window with a picker for item and , entry for a quantity
    }
    void NameSelector(ObservableCollection<object> items)
    {
        var listedItems = new List<Item>(items.Select(item => item as Item).OrderBy(i => i.Name));
        items.Clear();
        foreach (var item in listedItems)
        {
            items.Add(item);
        }
    }
    void QuantitySelector(ObservableCollection<object> items)
    {
        var listedItems = new List<Item>(items.Select(item => item as Item).OrderByDescending(i => i.Qty));
        items.Clear();
        foreach (var item in listedItems)
        {
            items.Add(item);
        }
    }
    #endregion
    #region Rollout window feature
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
    #endregion

}
