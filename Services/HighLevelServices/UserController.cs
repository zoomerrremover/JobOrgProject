using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Models.Misc;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.Services.HighLevelServices
{
    public class UserController :IUserController
    {
        public int VisibilityLevel { get => User.Position.VisibilityLevel; }
        List<Rule> Permissions { get => User.Position.Permissions; }
        IUser User { get;set; }
        public void CreateHistoryRecord(Thing Object, HistoryActionType type, string propertyName = null, object value = null, object value2 = null)
        {
            var newHistoryRecord = new HistoryRecord()
            {
                User = User,
                ActionType = type,
                FieldName = propertyName,
                Value = value.ToString(),
                Value2 = value2.ToString()
            };
        }
        public bool GetPermission(Thing Object, RuleType Type)
        {
            if (Object.Equals(User))
            {
                return true;
            }
            else
            {
                var type = Object.GetType();
                var permission = Permissions.Find(p=>p.Model == type);
                return permission.Status.Contains(Type);
            }
        }

        public bool GetPermission(Type Object, RuleType Type)
        {
            throw new NotImplementedException();
        }

        public void SetPermissions(IUser user)
        {
            User = user;
        }
    }
}
