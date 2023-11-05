
namespace TheJobOrganizationApp.Models
{
    public interface IUser
    {
        public string password { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
