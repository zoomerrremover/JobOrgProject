using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls
{
    public class PermissionEditor
    {
        public PermissionEditor(Type modelBinded)
        {
            modelName = modelBinded.Name;
            CreatePermission = userController.GetPermission(modelBinded, RuleType.Create);
            CreatePermission = userController.GetPermission(modelBinded, RuleType.Create);
            DeletePermission = userController.GetPermission(modelBinded, RuleType.Delete);
        }
        public string modelName { get; set; }
        public bool CreatePermission { get; set; }
        public bool DeletePermission { get; set; }

    }
}
