
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Models.UtilityClasses;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class TimeBasedVM : ModelView
{
    #region CTORS
    new public ITimeBased BindingObject { get; set; }
    public TimeBasedVM(ITimeBased BindingObject)
    {
        this.BindingObject = BindingObject;
        EditPermission = userController.GetPermission(BindingObject as Thing, RuleType.Edit);   
    }
    public void InitializeData()
    {
        DisplayableStartDate = BindingObject.StartTime;
        DisplayableFinishDate = BindingObject.FinishTime;
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
        if (EditPermission && !IsLoading) { 
            IsLoading = true;
            InputTransparency = !InputTransparency;
            if (InputTransparency)
            {
                var localFinishTime = new DateTime(DisplayableFinishDate.Year,
                                        DisplayableFinishDate.Month,
                                        DisplayableFinishDate.Day,
                                        DisplayableFinishTime.Hours,
                                        DisplayableFinishTime.Minutes, 0);
                var localStartTime = new DateTime(DisplayableStartDate.Year,
                    DisplayableStartDate.Month,
                    DisplayableStartDate.Day,
                    DisplayableStartTime.Hours,
                    DisplayableStartTime.Minutes, 0);
                if (localStartTime < localFinishTime)
                {
                    if (localFinishTime != BindingObject.FinishTime)
                    {
                        userController.CreateHistoryRecord(BindingObject as Thing, Models.Misc.HistoryActionType.Changed,
                            "finish time", BindingObject.FinishTime.ToString("t/d"), localFinishTime.ToString("t/d"));
                        BindingObject.FinishTime = localFinishTime;
                    }
                    if (localStartTime != BindingObject.StartTime)
                    {
                        userController.CreateHistoryRecord(BindingObject as Thing, Models.Misc.HistoryActionType.Changed,
                            "start time", BindingObject.StartTime.ToString("t/d"), localStartTime.ToString("t/d"));
                        BindingObject.FinishTime = localFinishTime;
                    }
                }
                else
                {
                    errorService.CallError("The dates does not match !");
                    InitializeData();
                }

            }
            IsLoading = false;
        }

    }

    #endregion
}
