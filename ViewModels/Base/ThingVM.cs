
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.BindableControls;
using CommunityToolkit.Maui.Core.Platform;

namespace TheJobOrganizationApp.ViewModels.Base
{

    public partial class ThingVM:ModelView
    {
        #region CTORS
        public new static ModelView CreateFromTheModel(Thing model)
        {
                var wm = new ThingVM(model);
                return wm;
            
        }
        public ThingVM(Thing BindingObject)
        {
            this.BindingObject = BindingObject;
            SetPermissions();
            HistoryCollectionView = new ModelCollectionView();

        }
        public virtual void LoadContent()
        {
            InitializeTextFields();
            InitializeHistoryCollectionView();
        }
        private void InitializeTextFields()
        {
            DisplayableName ??= string.Empty;
            DisplayableDescription ??= string.Empty;
            DisplayableName = BindingObject.Name;
            DisplayableDescription = BindingObject.Description;
            DisplayableID = BindingObject.Id.ToString();
        }
        #endregion
        #region Name
        [ObservableProperty]
        string displayableName;
        [ObservableProperty]
        bool nameInEditMode = true;
        [RelayCommand]
        protected virtual void NameEditButtonPressed()
        {
            NameInEditMode = !NameInEditMode;
            if (NameInEditMode && BindingObject.Name != DisplayableName)
            {
                if(DisplayableName.Length==0 || DisplayableDescription.Length>24)
                {
                    errorService.CallError("Name length is not right !");
                    DisplayableName = BindingObject.Name;
                }
                else
                {
                    CreateChangeHistoryRecord($"{nameof(BindingObject.Name)}", BindingObject.Name, DisplayableName);
                    BindingObject.Name = DisplayableName;
                }
            }
        }
        protected virtual void CreateChangeHistoryRecord(string property, string oldValue = null, string newValue = null)
        {
            userController.CreateHistoryRecord(BindingObject, Models.Misc.HistoryActionType.Changed, property, oldValue, newValue);
            InitializeHistoryCollectionView();
        }
        #endregion
        #region ID
        [ObservableProperty]
        public string displayableID;
        #endregion
        #region Description
        [ObservableProperty]
        string displayableDescription;
        [ObservableProperty]
        bool descriptionInEditMode = true;

        [RelayCommand]
        protected virtual void DescriptionEditButtonPressed()
        {
            DescriptionInEditMode = !DescriptionInEditMode;
            if (DescriptionInEditMode && BindingObject.Description != DisplayableDescription)
            {
                if (DisplayableDescription.Length == 0 || DisplayableDescription.Length > 300)
                {
                    errorService.CallError("Description length is not right !");
                    DisplayableDescription = BindingObject.Description;
                }
                CreateChangeHistoryRecord($"{nameof(BindingObject.Description)}", BindingObject.Description , DisplayableDescription);
                BindingObject.Description = DisplayableDescription;
            }
        }
        #endregion
        #region History
        public ModelCollectionView HistoryCollectionView { get; set; }

        void InitializeHistoryCollectionView()
        {
            HistoryCollectionView.Initiate(BindingObject.History);

        }

        #endregion
    }
}
