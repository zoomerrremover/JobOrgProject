
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IUserPermissionController
    {
        public int VisibilityLevel { get;}
        public void SetPermissions(IUser user);
        public bool GetPermission(Thing Object, RuleType Type);

    }
}
