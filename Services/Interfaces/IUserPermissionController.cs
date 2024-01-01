
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IUserPermissionController
    {
        public int VisibilityLevel { get;}

        public List<Rule> Permissions { get;}

        public void SetPermissions(IUser user);

    }
}
