
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;
[DetailsViewModel(ClassLinked = typeof(Job))]
public partial class JobProxy:ThingProxy
{
    new public Job BindingObject { get; set; }
    // CTORS
    //------------------------------------------------------------------------------------------------
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Job)
        {
            var wm = new JobProxy(model as Job);
            return wm;
        }
        else return null;
    }
    public JobProxy(Job item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }
    void Initialize()
    {

    }
    //------------------------------------------------------------------------------------------------
    public ObservableCollection<Contractor> Contractors { get; set; }
    [ObservableProperty]
    Contractor pickedContractor;
    //-------------------------------------------------------------------------------------------
    public ObservableCollection<Assignment> Assignments { get; set; }
    

    


}
