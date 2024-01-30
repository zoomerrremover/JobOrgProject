using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;
using TheJobOrganizationApp.ViewModels.Base;

namespace TheJobOrganizationApp.ViewModels.BindableControls
{
    public class PermissionEditor
    {
        public PermissionEditor(Position position,Type modelBinded)
        {
            Rule positionRule = position.Permissions.Single(r=>r.Model==modelBinded);
            modelName = modelBinded.Name;
            model = modelBinded;
            CreatePermission = positionRule is not null ? positionRule.Status.Contains(RuleType.Create) : false;
            EditPermission = positionRule is not null ? positionRule.Status.Contains(RuleType.Edit) : false;
            DeletePermission = positionRule is not null ? positionRule.Status.Contains(RuleType.Delete) : false;
        }
        public Rule ProcessPermissions()
        {
            var localRule = new Rule() { Model = model,
                                         Status = new()};
            if (EditPermission)
            {
                localRule.Status.Add(RuleType.Edit);
            }
            if (CreatePermission)
            {
                localRule.Status.Add(RuleType.Create);
            }
            if (DeletePermission)
            {
                localRule.Status.Add(RuleType.Delete);
            }
            return localRule;
        }
        Type model { get; set; }
        public string modelName { get; private set; }
        public bool EditPermission { get; set; }
        public bool CreatePermission { get;set; }
        public bool DeletePermission { get;set; }

    }
}
