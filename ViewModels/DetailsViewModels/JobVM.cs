
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
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

    public override void LoadContent()
    {
        base.LoadContent();
        InitializeAssignments();
        InitializePlacePicker();
    }
    void Initialize()
    {
        InitializeContractorPicker();
        var permission = userController.GetPermission(typeof(Assignment), RuleType.Create);
        AssignmentCollectionView = new ModelCollectionView()
                                       .WithAddButton(permission, CreateAssignment)
                                       .WithFilters(("Name", NameSelector),
                                                    ("Most Recent", TimeMostRecentSelector),
                                                    ("Least Recent", TimeLessRecentSelector));
        PlacePickerVM = new StringPickerVM()
                .WithPermissions(EditPermission)
                .WithNoneOption()
                .WithAction(ChangePlaceAction);
        void ChangePlaceAction(string oldValue, string newValue)
        {
            var localPlaces = dataStorage.GetItems<Place>().Select(job => job as Thing).ToObservableCollection();
            var newPlace = newValue != "None" ? localPlaces.Single(job => job.Name == newValue) as Place : null;
            CreateChangeHistoryRecord("place", oldValue, newValue);
            BindingObject.Place = newPlace;
        }
        InitializeTimeBasedVM();
    }

    #endregion
    #region Contractor
    public StringPickerVM ContractorPickerVM { get; set; }
    void InitializeContractorPicker()
    {

        var localContractors = dataStorage.GetItems<Contractor>().Select(job => job as Thing).ToObservableCollection();
        var initialValue = BindingObject.Contractor;
        ContractorPickerVM = new StringPickerVM(localContractors, initialValue)
            .WithPermissions(EditPermission)
            .WithNoneOption()
            .WithAction(ChangeContractor);
        void ChangeContractor(string oldValue, string newValue)
        {
            var newContractor = newValue != "None" ? localContractors.Single(job => job.Name == newValue) as Contractor : null;
            CreateChangeHistoryRecord("contractor",oldValue, newValue);
            BindingObject.Contractor = newContractor;
        }
    }
    #endregion
    #region Place
    public StringPickerVM PlacePickerVM { get; set; }
    void InitializePlacePicker()
    {

        var localPlaces = dataStorage.GetItems<Place>().Select(job => job as Thing).ToObservableCollection();
        var initialValue = BindingObject.Place;
        PlacePickerVM.InitializeContent(localPlaces, initialValue);
    }
    #endregion
    #region Time
    public TimeBasedVM TimeBasedVM { get; set; }
    void InitializeTimeBasedVM()
    {
        TimeBasedVM = new(BindingObject);
    }
    #endregion
    #region Assignments
    public ModelCollectionView AssignmentCollectionView { get; set; }
    void InitializeAssignments()
    {
        AssignmentCollectionView.Initiate(BindingObject.Tasks);
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



}
