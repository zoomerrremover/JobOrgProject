
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Models.Misc;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.UtilityClasses;

namespace TheJobOrganizationApp.Services.LowLeveLServices
{
    public class UserController : IUserPermissionController, IUserController
    {
        public int VisibilityLevel { get => User.Position.VisibilityLevel; }

        public List<Rule> Permissions { get => User.Position.Permissions; }

        IUser User { get; set; }

        public void CreateHistoryRecord(Thing Object, HistoryActionType type,string propertyName = null,object value = null, object value2 = null)
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

        public void SetPermissions(IUser user)
        {
            User = user;
        }
    }
}
