
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasItemsVM : ModelView
{
    #region CTORS
    new IHasItems BindingObject;
    public HasItemsVM(IHasItems BindingObject)
    {
        this.BindingObject = BindingObject;
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
        var permission = userController.GetPermission(typeof(Assignment), RuleType.Create);
        ItemsCollectionView = new ModelCollectionView(BindingObject.Items)
                                       .WithAddButton(permission, AddButton)
                                       .WithFilters(("Name", NameSelector),
                                                    ("Quantity", QuantitySelector));
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
