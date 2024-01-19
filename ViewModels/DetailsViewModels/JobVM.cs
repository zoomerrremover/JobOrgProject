
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
        InitializeAssignments();
        InitializeTimeBasedVM();
        InitializePlace();
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
                                       .WithFilters(("Name",NameSelector),
                                                    ("Most Recent",TimeMostRecentSelector),
                                                    ("Least Recent",TimeLessRecentSelector));
    }

    void CreateAssignment()
    {
        var newAssignment = new Assignment();
        BindingObject.Tasks.Add(newAssignment);
        pageFactory.MakeACreatePage(typeof(Assignment), newAssignment);
    }
    void NameSelector(ObservableCollection<object> holders)
    {
        holders.OfType<Thing>().OrderBy(assignment => assignment.Name);

    }
    void TimeMostRecentSelector(ObservableCollection<object> holders)
    {
        holders.OfType<Assignment>().OrderBy(assignment => assignment.FinishTime);
    }
    void TimeLessRecentSelector(ObservableCollection<object> holders)
    {
        holders.OfType<Assignment>().OrderByDescending(assignment => assignment.FinishTime);
    }
    #endregion
    #region Time
    public TimeBasedVM TimeBasedVM { get;set; }
    void InitializeTimeBasedVM()
    {
        TimeBasedVM = new(BindingObject);
    }
    #endregion
    #region Place
    ObservableCollection<Place> places;
    public ObservableCollection<string> DisplayablePlaces { get => places.Select(place => place.ToString()).ToObservableCollection(); }
    [ObservableProperty]
    string pickedPlaced;
    partial void OnPickedPlacedChanged(string value)
    {
        BindingObject.Place = value == "None" ? null:places.Single(place => place.Name == value);
    }
    void InitializePlace()
    {
        places = dataStorage.GetItems<Place>();
        PickedPlaced = BindingObject.Place.ToString();
    }
    #endregion



}
