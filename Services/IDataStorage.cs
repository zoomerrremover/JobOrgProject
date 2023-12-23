
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;


namespace TheJobOrganizationApp.Services
{
    public interface IDataStorage: Iintializable
    {
        public void RegisterModel(Type type)
        {
        }
        public void AddThing<T>(T thing)where T : Thing
        {
        }
        public void AddThing<T>(IEnumerable<T> thing) where T : Thing
        {
        }

        public void RemoveThing<T>(T thing)
        {

        }
        public bool InitializeDatabase()
        {
            return true;
        }

        public ObservableCollection<T> GetItems<T>(Type key = null) where T : Thing
        {
            var items = new ObservableCollection<T>();
            return items;
        }
        public ObservableCollection<Thing> GetItemsWithInterface<T>(string key = null)
        {
            var items = new ObservableCollection<Thing>();
            return items;
        }

    }

    
}
