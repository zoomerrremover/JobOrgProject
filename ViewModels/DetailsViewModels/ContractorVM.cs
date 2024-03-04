

using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels;

[DetailsViewModel(ClassLinked = typeof(Contractor))]
public partial class ContractorVM:ThingVM
{
    #region CTORS
    new public Contractor BindingObject { get; set; }
    public new static ModelView CreateFromTheModel(Thing model)
    {
        if (model is Contractor)
        {
            var wm = new ContractorVM(model as Contractor);
            return wm;
        }
        else return null;
    }
    public ContractorVM(Contractor item) : base(item)
    {
        BindingObject = item;
        Initialize();
    }
    void Initialize()
    {
        HasContactsVM = new(BindingObject);
        var permission = EditPermission && userController.GetPermission(typeof(Job), RuleType.Create);
        JobsCollectionView = new ModelCollectionView().WithAddButton(permission, CreateJob);
    }
    public override void LoadContent()
    {
        base.LoadContent();
        InitializeJobs();
    }

    #endregion
    #region Jobs List
    private void InitializeJobs()
    {
        var jobs = from job in dataStorage.GetItems<Job>()
                   where job.Contractor == BindingObject
                   select job;
        JobsCollectionView.Initiate(jobs,typeof(Job));
    }
    private void CreateJob()
    {
        pageFactory.MakeACreatePage(typeof(Job),new Job { Contractor = BindingObject});
    }
    public ModelCollectionView JobsCollectionView { get; set; }
    #endregion
    #region Contacts
    public HasContactsVM HasContactsVM { get; set; }
    #endregion
}
