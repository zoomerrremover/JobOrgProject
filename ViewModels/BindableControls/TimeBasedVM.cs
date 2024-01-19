
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class TimeBasedVM : ModelView
{
// CTORS
//----------------------------------------------------------------------------------------------------------
    [ObservableProperty]
    ITimeBased bindingObject;
    public TimeBasedVM(ITimeBased BindingObject)
    {
        this.BindingObject = BindingObject;
        InitializeData();
    }

    void InitializeData()
    {
        DisplayableStartDate = BindingObject.StartTime;
        DisplayableStartDate = BindingObject.FinishTime;
    }

    //----------------------------------------------------------------------------------------------------------
    [ObservableProperty]
    DateTime displayableStartDate;
    [ObservableProperty]
    DateTime displayableFinishDate;
    [ObservableProperty]
    bool inputTransparency = true;

    [RelayCommand]
    void OnSaveButtonPressed()
    {
        if(!InputTransparency)
        {
            if (DisplayableStartDate < DisplayableFinishDate)
            {
                BindingObject.FinishTime = DisplayableFinishDate;
                BindingObject.StartTime = DisplayableStartDate;
                dataStorage.TriggerUpdate(BindingObject);

            }
            else
            {
                DisplayTimeError.Invoke();
                DisplayableFinishDate = BindingObject.FinishTime;
                DisplayableFinishDate = BindingObject.StartTime;
            }
        }
        else
        {
            InputTransparency = false;
        }

    }

    public event Action DisplayTimeError = () => { };
    
}
