using CommunityToolkit.Mvvm.ComponentModel;
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;
using TheJobOrganizationApp.ViewModels.ModelWrappers;

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
            InitializePermissionEditors();
            WorkerCollectionView = new ModelCollectionView()
                .WithEditButton(EditPermission, EditButtonPressed);
        }
        public override void LoadContent()
        {
            base.LoadContent();
            InitializePermissionEditors();
            InitializeWrokerColView();
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
            CreateChangeHistoryRecord("permissions");

        }
        #endregion
        #region Workers with Pos
        public bool InEditMode { get; set; } = false;

        IEnumerable<PickableWorker> workers;
        void InitializeWrokerColView()
        {
            var Workers = dataStorage.GetItems<Worker>();
            workers = Workers.Select(w=>new PickableWorker(w,w.Position == BindingObject));
            WorkerCollectionView.Initiate(workers);
        }
        void EditButtonPressed()
        {
            InEditMode = !InEditMode;
            if (EditPermission && !InEditMode)
            {
                foreach(var pickableWorker in workers)
                {
                    if (pickableWorker.data)
                    {
                        userController.CreateHistoryRecord(pickableWorker.model, Models.Misc.HistoryActionType.Changed,
                            "position", pickableWorker.model.Position, BindingObject);
                        pickableWorker.model.Position = BindingObject;
                    }
                    else if(!pickableWorker.data && pickableWorker.model.Position == BindingObject) {
                        userController.CreateHistoryRecord(pickableWorker.model, Models.Misc.HistoryActionType.Changed,
    "position", pickableWorker.model.Position, "None");
                        pickableWorker.model.Position = null;
                    }
                }
            }
        }
        public ModelCollectionView WorkerCollectionView { get; set; }

        #endregion

    }
}
