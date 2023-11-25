
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;


namespace TheJobOrganizationApp.Services
{
    public interface IDataStorage
    {

        public Dictionary<string, ObservableCollection<object>> ObjectKeeper { get; }

        public void RegisterModel(Type type)
        {
            ObjectKeeper[nameof(type)] = new ObservableCollection<object>();
        }
        
            
    }

    
}
