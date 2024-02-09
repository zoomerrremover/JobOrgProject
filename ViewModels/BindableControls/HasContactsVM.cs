
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls;

public partial class HasContactsVM
    : ModelView
{
    new IHasContacts BindingObject;
    public HasContactsVM(IHasContacts BindingObject)
    {
        this.BindingObject = BindingObject;
        Cell = BindingObject.PhoneNumber;
        Buisness = BindingObject.Buissness;
        Email = BindingObject.Email;
    }
    [ObservableProperty]
    string cell;
    [ObservableProperty]
    string buisness;
    [ObservableProperty]
    string email;
    [RelayCommand]
    void OnEditButtonPressed()
    {
        if(EditPermission && !IsLoading)
        {
            IsLoading = true;
            const string phoneNumberPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            const string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            Regex emailRegex = new Regex(emailPattern);
            Regex phoneNumberRegex = new Regex(phoneNumberPattern);
            if(BindingObject.Buissness != Buisness)
            {
                if(phoneNumberRegex.IsMatch(Buisness))
                {
                    userController.CreateHistoryRecord(BindingObject as Thing, Models.Misc.HistoryActionType.Changed,
                        "buisiness phone number",BindingObject.Buissness,Buisness);
                    BindingObject.Buissness = Buisness;
                }
                else
                {
                    errorService.CallError("Buisiness number is not in the right format.");
                    Buisness = BindingObject.Buissness;
                }
            }
            if (BindingObject.Email != Buisness)
            {
                if (emailRegex.IsMatch(Email))
                {
                    userController.CreateHistoryRecord(BindingObject as Thing, Models.Misc.HistoryActionType.Changed,
                        "email", BindingObject.Email, Email);
                    BindingObject.Email = Email;
                }
                else
                {
                    errorService.CallError("Email is not in the right format.");
                    Email = BindingObject.Email;
                }
            }
            if (BindingObject.PhoneNumber != Cell)
            {
                if (phoneNumberRegex.IsMatch(Cell))
                {
                    userController.CreateHistoryRecord(BindingObject as Thing, Models.Misc.HistoryActionType.Changed,
                        "cell number", BindingObject.PhoneNumber, Cell);
                    BindingObject.PhoneNumber = Cell;
                }
                else
                {
                    errorService.CallError("Cell is not in the right format.");
                    Cell = BindingObject.PhoneNumber;
                }
            }
            IsLoading = false;
        }
    }

}
