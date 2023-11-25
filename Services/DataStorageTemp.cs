
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services;

public class DataStorageTemp : BaseViewModel ,IDataStorage

{
    public Dictionary<string, ObservableCollection<object>> ObjectKeeper { get; }

    public void RegisterModel(Type type)
    {
        ObjectKeeper[nameof(type)] = new ObservableCollection<object>();
    }

}
