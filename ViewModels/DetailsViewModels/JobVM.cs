
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;
[DetailsViewModel(ClassLinked = typeof(Job))]
public partial class JobVM:ThingVM
{
    #region CTORS
    new public Job BindingObject { get; set; }
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Job)
        {
            var wm = new JobVM(model as Job);
            return wm;
        }
        else return null;
    }
    public JobVM(Job item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }
    void Initialize()
    {
        InitializeContractors();
    }

    #endregion
    #region Contractor
    void InitializeContractors()
    {
        Contractors = dataStorage.GetItems<Contractor>();
        PickedContractor = BindingObject.Contractor;
    }
    public ObservableCollection<Contractor> Contractors { get; set; }
    [ObservableProperty]
    Contractor pickedContractor;
    partial void OnPickedContractorChanged(Contractor value)
    {
        BindingObject.Contractor = value;
    }
    #endregion
    #region Assignments
    public ModelCollectionView AssignmentCollectionView { get; set; }
    void InitializeAssignments()
    {
        var permission = userController.GetPermission(typeof(Assignment), RuleType.Create);
        AssignmentCollectionView = new ModelCollectionView(BindingObject.Tasks)
                                       .WithAddButton(permission,CreateAssignment)
                                       .WithFilters();
    }

    void CreateAssignment()
    {
        pageFactory.MakeACreatePage(typeof(Assignment), new Job { Contractor = BindingObject });
    }
    void NameSelector(ObservableCollection<Assignment> holders)
    {
        holders.OrderBy(assignment => assignment.Name);

    }
    void TimeMostRecentSelector(ObservableCollection<Assignment> holders)
    {
        holders.OrderBy(assignment => assignment.FinishTime);
    }
    void TimeLessRecentSelector(ObservableCollection<Assignment> holders)
    {
        holders.OrderByDescending(assignment => assignment.FinishTime);
    }
    #endregion




}
