﻿
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasItemsVM : ModelView
{
    [ObservableProperty]
    IHasItems bindingObject;
    [ObservableProperty]
    public ObservableCollection<Item> displayedItems = new();

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
    delegate void ListSelector(ObservableCollection<Item> items);
    Dictionary<string, ListSelector> filterSelectorMethods = new();
    [ObservableProperty]
    List<string> avaliableFilters = new();
    public HasItemsVM(IHasItems BindingObject)
    {
        this.BindingObject = BindingObject;
        Initiate();
    }
    public void Initiate()
    {
        RolloutButtonSwitch.SwitchOff += ExtendScreen;
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

}