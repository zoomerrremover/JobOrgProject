
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
        Declare();
    }

    public override void LoadContent()
    {
        base.LoadContent();
        TimeBasedVM.InitializeData();
        InitializeAssignments();
        InitializePlacePicker();
        InitializeContractorPicker();
    }
    void Declare()
    {
        DeclareContractorPicker();
        DeclarePlacePicker();
        DecalreTimeBasedVM();
    }

    private void DeclarePlacePicker()
    {
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
            if (EditPermission)
            {
                var localPlaces = dataStorage.GetItems<Place>().Select(job => job as Thing).ToObservableCollection();
                var newPlace = newValue != "None" ? localPlaces.Where(job => job.Name == newValue).FirstOrDefault() as Place : null;
                CreateChangeHistoryRecord("place", oldValue, newValue);
                BindingObject.Place = newPlace;
            }
        }
    }

    #endregion
    #region Contractor
    public StringPickerVM ContractorPickerVM { get; set; }
    void DeclareContractorPicker()
    {
        ContractorPickerVM = new StringPickerVM()
            .WithPermissions(EditPermission)
            .WithNoneOption()
            .WithAction(ChangeContractor);
        void ChangeContractor(string oldValue, string newValue)
        {
            if (EditPermission && !IsLoading)
            {
                var localContractors = dataStorage.GetItems<Contractor>().Select(job => job as Thing).ToObservableCollection();
                var newContractor = newValue != "None" ? localContractors.Where(job => job.Name == newValue).FirstOrDefault() as Contractor : null;
                CreateChangeHistoryRecord("contractor", oldValue, newValue);
                BindingObject.Contractor = newContractor;
            }
            else
            {
                ContractorPickerVM.PickedObject = oldValue;
            }
        }
    }
    void InitializeContractorPicker()
    {
        var localContractors = dataStorage.GetItems<Contractor>().Select(job => job as Thing).ToObservableCollection();
        var initialValue = BindingObject.Contractor;
        ContractorPickerVM.InitializeContent(localContractors,initialValue);
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
    void DecalreTimeBasedVM()
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
