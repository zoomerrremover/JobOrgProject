﻿
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.Models;

public abstract class Thing

{
    public Guid Id { get; set; }
    public string Name { get; set; } = "Enter the name";

    public string Description { get; set; } = string.Empty;

    public ObservableCollection<HistoryRecord> History { get; set; } = new();


    public Thing()
    {
         Id = Guid.NewGuid();
    }


    public override bool Equals(object obj)
    {
       if(obj == null) return false;
       if(obj as  Thing == null) return false;
       var newObj = obj as Thing;
        if(newObj.Id != Id) return false;
        else return true;
    }
    public override string ToString()
    {
        return Name;
    }
   
}
