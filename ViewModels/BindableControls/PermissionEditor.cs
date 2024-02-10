using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.ViewModels.BindableControls
{
    public partial class PermissionEditor:ObservableObject
    {
        Rule positionRule;
        public PermissionEditor(Position position,Type modelBinded)
        {
            modelName = modelBinded.Name;
            model = modelBinded;
            try
            {
                positionRule = position.Permissions.Where(r => r.Model == modelBinded).First();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                CreatePermission = positionRule is not null ? positionRule.Status.Contains(RuleType.Create) : false;
                EditPermission = positionRule is not null ? positionRule.Status.Contains(RuleType.Edit) : false;
                DeletePermission = positionRule is not null ? positionRule.Status.Contains(RuleType.Delete) : false;
            }
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
        [ObservableProperty]
        public bool editPermission;
        [ObservableProperty]
        public bool createPermission;
        [ObservableProperty]
        public bool deletePermission;
    }
}
