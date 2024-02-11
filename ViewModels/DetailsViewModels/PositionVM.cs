using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
            DeclarePermissionEditor();
            DeclareWorkerCollectionView();
        }

        private void DeclarePermissionEditor()
        {
            PermissionEditors = new();
            PermissionEditor.SetPermission(EditPermission);
        }

        private void DeclareWorkerCollectionView()
        {
            WorkerCollectionView = new ModelCollectionView()
                .WithEditButton(EditPermission, EditButtonPressed);
        }

        private async void WorkerActivatedAction(PickableWorker pickableWorker)
        {
            if (InEditMode && EditPermission)
            {
                pickableWorker.data = !pickableWorker.data;
                if (pickableWorker.data)
                {
                    userController.CreateHistoryRecord(pickableWorker.model, Models.Misc.HistoryActionType.Changed,
                        "position", pickableWorker.model.Position.ToString(), BindingObject.ToString());
                    pickableWorker.model.Position = BindingObject;
                }
                else if (!pickableWorker.data && pickableWorker.model.Position == BindingObject)
                {
                    userController.CreateHistoryRecord(pickableWorker.model, Models.Misc.HistoryActionType.Changed,
"position", pickableWorker.model.Position.ToString(), "None");
                    pickableWorker.model.Position = null;
                }
                await Task.Run(InitializeWrokerColView);
            }
            
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
            foreach(var modelType in reflectionContent.Models)
            {
                var permEditorLoc = new PermissionEditor(BindingObject,modelType);
                PermissionEditors.Add(permEditorLoc);
            }
        }
        public ObservableCollection<PermissionEditor> PermissionEditors { get; set; }

        [ObservableProperty]
        bool permissionsInEditMode = true;

        [RelayCommand]
        async Task OnPermissionEditButtonPressed()
        {
            PermissionsInEditMode = !PermissionsInEditMode;
            if (!PermissionsInEditMode)
            {
                await Task.Run(SavePermissions);
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
            PickableWorker.ActivatedAction = WorkerActivatedAction;
            var Workers = dataStorage.GetItems<Worker>();
            workers = Workers.Select(w=>new PickableWorker(w,w.Position == BindingObject));
            WorkerCollectionView.Initiate(workers);
        }
        void EditButtonPressed()
        {
            InEditMode = !InEditMode;
        }
        public ModelCollectionView WorkerCollectionView { get; set; }

        #endregion

    }
}
