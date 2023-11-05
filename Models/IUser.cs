
namespace TheJobOrganizationApp.Models
{
    public interface IUser
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string EmailForLogIn { get; set; }
    }
}
