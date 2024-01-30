using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.ViewModels.Base;
using TheJobOrganizationApp.ViewModels.BindableControls;

namespace TheJobOrganizationApp.ViewModels.DetailsViewModels
{
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

        }
        #endregion
        #region Permissions
        public void InitializePermissionEditors()
        {
            PermissionEditors = new List<PermissionEditor>();
            foreach(var modelType in reflectionContent.Models)
            {
                var permEditorLoc = new PermissionEditor(modelType);
                PermissionEditors.Add(permEditorLoc);
            }
        }
        public List<PermissionEditor> PermissionEditors { get; set; }
        [RelayCommand]
        void SaveButtonPresses()
        {
            foreach(PermissionEditor editor in PermissionEditors)
            {
                if(editor.EditPermission = true)
                BindingObject.Permissions
            }
        }
        #endregion
    }
}
