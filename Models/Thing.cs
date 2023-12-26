
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.Models;

public abstract partial class Thing:ObservableObject

{
    public Guid Id { get; set; }
    [ObservableProperty]
    public string name;

    public string Description { get; set; }

    public ObservableCollection<HistoryRecord> History;


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
   
    public static Action<Thing> updateActionInvoke;
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        updateActionInvoke.Invoke(this);
        base.OnPropertyChanged(e);
    }
}
