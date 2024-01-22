
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

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
            InitializeComponents();

        }
        public void InitializeComponents()
        {
            DisplayableName = BindingObject.Name;
            DisplayableDescription = BindingObject.Description;
            DisplayableID = BindingObject.Id.ToString();
            InitializeHistoryCollectionView();
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
            if (NameInEditMode)
            {
                BindingObject.Name = DisplayableName;
            }
            dataStorage.TriggerUpdate(BindingObject);
        }
        #endregion
        #region ID
        public string DisplayableID { get; private set; }
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
            if (DescriptionInEditMode)
            {
                BindingObject.Description = DisplayableDescription;
            }
            dataStorage.TriggerUpdate(BindingObject);
        }
        #endregion
        #region History
        public ModelCollectionView HistoryCollectionView { get; set; }

        void InitializeHistoryCollectionView()
        {
            HistoryCollectionView = new ModelCollectionView(BindingObject.History);

        }
        #endregion
    }
}
