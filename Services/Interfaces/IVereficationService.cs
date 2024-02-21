

namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IVereficationService
    {
        public Task<bool> LogInToServer(string serverID);

        public Task<bool> LogOut();

        public Task<bool> LogInToAccount(string userName,string password);
    }
}
