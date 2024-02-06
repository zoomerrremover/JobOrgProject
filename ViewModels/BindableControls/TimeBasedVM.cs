
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class TimeBasedVM : ModelView
{
    #region CTORS
    new ITimeBased BindingObject;
    public TimeBasedVM(ITimeBased BindingObject)
    {
        this.BindingObject = BindingObject;
        InitializeData();
    }
    void InitializeData()
    {
        DisplayableStartDate = BindingObject.StartTime;
        DisplayableStartDate = BindingObject.FinishTime;
        DisplayableStartTime = new TimeSpan(BindingObject.StartTime.Hour, BindingObject.StartTime.Minute,BindingObject.StartTime.Second);
        DisplayableFinishTime = new TimeSpan(BindingObject.FinishTime.Hour, BindingObject.FinishTime.Minute, BindingObject.FinishTime.Second);
    }

    #endregion
    #region Time
    [ObservableProperty]
    DateTime displayableStartDate;
    [ObservableProperty]
    DateTime displayableFinishDate;
    [ObservableProperty]
    TimeSpan displayableStartTime;
    [ObservableProperty]
    TimeSpan displayableFinishTime;
    [ObservableProperty]
    bool inputTransparency = true;

    [RelayCommand]
    void OnSaveButtonPressed()
    {
        if (EditPermission) { 
            InputTransparency = !InputTransparency;
            if (InputTransparency)
            {
                if (DisplayableStartDate < DisplayableFinishDate)
                {
                    BindingObject.FinishTime = new(DisplayableFinishDate.Year,
                        DisplayableFinishDate.Month,
                        DisplayableFinishDate.Day,
                        DisplayableFinishTime.Hours,
                        DisplayableFinishTime.Minutes, 0);
                    BindingObject.StartTime = new(DisplayableStartDate.Year,
                        DisplayableStartDate.Month,
                        DisplayableStartDate.Day,
                        DisplayableStartTime.Hours,
                        DisplayableStartTime.Minutes, 0);
                }
                else
                {
                    errorService.CallError("The dates does not match !");
                    InitializeData();
                }
            }
        }

    }

    #endregion
}
