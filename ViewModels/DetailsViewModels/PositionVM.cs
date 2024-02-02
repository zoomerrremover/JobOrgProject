using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels
{
    [DetailsViewModel(ClassLinked = typeof(Position))]
    public partial class PositionVM:ThingVM
    {
        #region CTORs
        new public Position BindingObject { get; set; }
        public new static ModelView CreateFromTheModel(Thing model)
        {
            var wm = new PositionVM(model as Position);
            return wm;

        }
        public PositionVM(Position BindingObject): base(BindingObject)
        {
            this.BindingObject = BindingObject;
            InitializeComponents();
            InitializePermissionEditors();
        }
        #endregion
        #region Permissions
        public void InitializePermissionEditors()
        {
            PermissionEditors = new List<PermissionEditor>();
            foreach(var modelType in reflectionContent.Models)
            {
                var permEditorLoc = new PermissionEditor(BindingObject,modelType);
                PermissionEditors.Add(permEditorLoc);
            }
        }
        public List<PermissionEditor> PermissionEditors { get; set; }

        [ObservableProperty]
        bool permissionsInEditMode;

        partial void OnPermissionsInEditModeChanged(bool value)
        {
            if (!value)
            {
                SavePermissions();
            }
        }
        void SavePermissions()
        {
            BindingObject.Permissions.Clear();
            foreach(PermissionEditor editor in PermissionEditors)
            {
                BindingObject.Permissions.Add(editor.ProcessPermissions());
            }
        }
        #endregion
        #region Workers with Pos
        void InitializeWrokerColView()
        {
            var Workers = dataStorage.GetItems<Worker>();
            var FilteredWorkers = Workers.Where(w => w.Position == BindingObject);
            WorkerCollectionView = new ModelCollectionView(FilteredWorkers)
                .WithEditButton(EditPermission);
        }
        public ModelCollectionView WorkerCollectionView { get; set; }

        #endregion

    }
}
