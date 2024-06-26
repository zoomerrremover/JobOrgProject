﻿
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp;

namespace TheJobOrganizationApp.Services;

public class DataStorageTemp : IDataStorage 

{
    IReflectionContent ReflectionContent;
    Dictionary<Type, ObservableCollection<Thing>> ObjectKeeper { get; set; } = new();

    public DateTime ContentVersion { get; set; }

    public void RegisterModel(Type type)
    {
        ObjectKeeper[type] = new ObservableCollection<Thing>();
    }
    public bool InitializeDatabase()
    {
        foreach (var model in ReflectionContent.Models)
        {
            RegisterModel(model);
        }
        return true;

    }
    public void RemoveThing(Thing thing)
    {
        var type = thing.GetType();
        ObjectKeeper[type].Remove(thing);
    }
    public void AddThing(Thing thing)
    {
        var type = thing.GetType();
        ObjectKeeper[type].Add(thing);
    }

    public void AddThing<T>(IEnumerable<T> thing)
    {
        if (thing.First().GetType().BaseType != typeof(Thing)) return;
        var type = thing.First().GetType();
        foreach(var model in thing)
        {
            ObjectKeeper[type].Add(model as Thing);
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
    public DataStorageTemp(IReflectionContent ReflectionContent)
    {
        this.ReflectionContent = ReflectionContent;
        Initialize();
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
        if (type.BaseType == typeof(Thing)) ObjectKeeper[type].CollectionChanged += action;
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
        ObjectKeeper[typeOfThing].TriggerEvent();    
    }

    public bool CompareContentVersion(DateTime CurrentContent)
    {
        throw new NotImplementedException();
    }
}
