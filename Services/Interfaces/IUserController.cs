
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Models.Misc;

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IUserController
    {
        public void CreateHistoryRecord(Thing Object, HistoryActionType type, string propertyName = null, object value = null, object value2 = null);
    }
}
