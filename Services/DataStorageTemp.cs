
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services;

public class DataStorageTemp : BaseViewModel ,IDataStorage 

{
    GlobalSettings globalSettings;
    Dictionary<Type, ObservableCollection<Thing>> ObjectKeeper { get; set; } = new();

    public void RegisterModel(Type type)
    {
        ObjectKeeper[type] = new ObservableCollection<Thing>();
    }
    public bool InitializeDatabase()
    {
        foreach (var model in globalSettings.Models)
        {
            RegisterModel(model);
        }
        return true;

    }
    public void RemoveThing<T>(T thing) where T : Thing
    {
        var Ts = typeof(T);
        ObjectKeeper[typeof(T)].Remove(thing);
    }
    public void AddThing<T>(T thing)where T : Thing
    {
        var Ts = typeof(T);
        ObjectKeeper[typeof(T)].Add(thing);
    }

    public void AddThing<T>(IEnumerable<T> thing) where T : Thing
    {
        var Ts = typeof(T);
        foreach(var model in thing)
        {
            ObjectKeeper[Ts].Add(model);
        }
    }

    public bool Initialize()
    {
        if (InitializeDatabase())
        {
            return true;
        }
        return false;
    }
    public DataStorageTemp(GlobalSettings settings)
    {
        globalSettings = settings;
    }

    public ObservableCollection<T> GetItems<T>(Type key = null)where T : Thing
    {
        var colToFill = new ObservableCollection<T>();
        key ??= typeof(T);
        foreach (var item in ObjectKeeper[key])
            {
                colToFill.Add((T)item);
            }
        ObjectKeeper[key].CollectionChanged += (s, e) =>
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (T item in e.NewItems)
                        colToFill.Add(item);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in e.OldItems)
                        colToFill.Remove(item);
                    break;
            }
        };
        return colToFill;
    }
    public ObservableCollection<Thing> GetItemsWithInterface<T>()
    {
        Type type = typeof(T);
        var colToFill = new ObservableCollection<Thing>();
        foreach (var list in ObjectKeeper.Keys)
        {
            if (list.GetInterfaces().Contains(type))
            {
                foreach (var item in ObjectKeeper[list])
                {
                    colToFill.Add(item);
                }
            }
        }
        return colToFill;
    }
    public void SubscribeForUpdates(NotifyCollectionChangedEventHandler action,Type type)
    {
        if (type.IsClass) ObjectKeeper[type].CollectionChanged += action;
        else if(type.IsInterface)
        {
            foreach(var collection in ObjectKeeper.Where(i => i.Key.GetInterfaces().Contains(type)))
            {
                collection.Value.CollectionChanged += action;
            }
        }
    }
    public void TriggerUpdate<T>(T objectKey = null)where T : class
    {
        var typeOfThing = objectKey is null?typeof(T):objectKey.GetType();
        ObjectKeeper[typeOfThing].Add(ObjectKeeper[typeOfThing].Last());
        ObjectKeeper[typeOfThing].Remove(ObjectKeeper[typeOfThing].Last());
    }



}
