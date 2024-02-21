
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Models.Misc;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IUserController
    {
        public IUser User { get; }
        public int VisibilityLevel { get; }
        public void SetPermissions(IUser user);
        public bool GetPermission(Thing Object, RuleType Type);
        public bool GetPermission(Type Object, RuleType Type);
        public void CreateHistoryRecord(Thing Object, HistoryActionType type, string propertyName = null, string value = null, string value2 = null);
    }
}
