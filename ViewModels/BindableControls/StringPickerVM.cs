using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.HighLevelServices;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

/// <summary>
/// This class should be binded to object picker . 
/// it Contains ceratin set of object displaying them as a string , when selection is changed it triggers
/// action added with ctor. 
/// </summary>
public partial class StringPickerVM:ObservableObject
{
    ObservableCollection<Thing> objects { get; init; }
    public ObservableCollection<string> DisplayableObjects {
        get => objects.Select(x => x.Name).ToObservableCollection();
            }

    [ObservableProperty]
    string pickedObject = "";

    public StringPickerVM(ObservableCollection<Thing> objectsBase,Thing InitialValue = null)
    {
        objects = objectsBase;
        pickedObject = InitialValue is null ? "None" : DisplayableObjects.Single(obj => InitialValue.Name == obj);
    }
    public StringPickerVM WithAction(Action<string,string> action)
    {
        ChoiceChangedAction = action;
        return this;
    }
    public StringPickerVM WithPermissions(bool EditPermission = false,bool AddPermision = false)
    {
        this.EditPermission = EditPermission;
        this.AddPermision = AddPermision;
        return this;
    }
    public StringPickerVM WithNoneOption()
    {
        NoneOptionEnabled = true;
        DisplayableObjects.Insert(0, "None");
        return this;
    } 
    public StringPickerVM WithAddButton(Action addButtonAction)
    {
        AddButtonActive = true;
        AddButtonAction = addButtonAction;
        return this;
    }
    partial void OnPickedObjectChanging(string oldValue, string newValue)
    {
        if (ChoiceChangedAction is not null) ChoiceChangedAction.Invoke(oldValue, newValue);
    }
    [RelayCommand]
    void AddButtonPressed()=> AddButtonAction.Invoke();

    Action AddButtonAction { get; set; }
    Action<string, string> ChoiceChangedAction { get; set; }
    
    public bool EditPermission { get; private set; }

    public bool AddPermision { get; private set; }

    public bool AddButtonActive {  get;private set; }

    public bool NoneOptionEnabled { get;private set; }


}
