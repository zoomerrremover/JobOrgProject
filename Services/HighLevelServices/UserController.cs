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
        public IUser User { get;private set; }
        public void CreateHistoryRecord(Thing Object, HistoryActionType type, string propertyName = null, string oldValue = null, string newValue = null)
        {
            var newHistoryRecord = new HistoryRecord()
            {
                User = User,
                Subject = Object,
                ActionType = type,
                FieldName = propertyName,
                OldValue = oldValue,
                NewValue = newValue
            };
            Object.History.Add(newHistoryRecord);
        }
        public bool GetPermission(Thing Object, RuleType Type)
        {
            if (Object != null && Type != null)
            {
                if (Object.Equals(User))
                {
                    return true;
                }
                else
                {
                    var type = Object.GetType();
                    var permissions = Permissions.Find(p => p.Model == type);
                    var permission = permissions is not null ? permissions.Status.Contains(Type) : false;
                    return permission;
                }
            }

            return false;
        }

        public bool GetPermission(Type Object, RuleType Type)
        {
            if (Object != null && Type != null)
            {
                var permissions = Permissions.Find(p => p.Model == Object);
                var permission = permissions is not null ? permissions.Status.Contains(Type) : false;
                return permission;
            }
            return false;
        }

        public void SetPermissions(IUser user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("User");
            }
            User = user;
        }
    }
}
