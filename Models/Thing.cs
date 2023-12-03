﻿
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.Models;

public abstract class Thing

{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public List<HistoryRecord> History { get; set; }

    protected static IDataStorage mainQuery;

    public Thing()
    {
         Id = Guid.NewGuid();
    }

    public bool Initialize(IDataStorage storange)
    {
        mainQuery = storange;
        return true;
    }

    public override bool Equals(object obj)
    {
       if(obj == null) return false;
       if(obj as  Thing == null) return false;
       var newObj = obj as Thing;
        if(newObj.Id != Id) return false;
        else return true;
    }
}
