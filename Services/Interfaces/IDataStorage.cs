
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TheJobOrganizationApp.Models;


namespace TheJobOrganizationApp.Services
{
    public interface IDataStorage
    {
        public DateTime ContentVersion { get; }
        public void RegisterModel(Type type);
        public void AddThing<T>(T thing) where T : Thing;
        public void AddThing<T>(IEnumerable<T> thing) where T : Thing;
        public void TriggerUpdate<T>(T key = null) where T : class;
        public void SubscribeForUpdates(NotifyCollectionChangedEventHandler action, Type type);
        public void RemoveThing<T>(T thing)where T : Thing;
        public bool InitializeDatabase()
        {
            return true;
        }
        public ObservableCollection<T> GetItems<T>(Type key = null) where T : Thing
        {
            var items = new ObservableCollection<T>();
            return items;
        }
        public ObservableCollection<Thing> GetItemsWithInterface<T>()
        {
            var items = new ObservableCollection<Thing>();
            return items;
        }
        public bool CompareContentVersion(DateTime CurrentContent);

    }

    
}
