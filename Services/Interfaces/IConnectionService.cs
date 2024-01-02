
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services
{
    public interface IConnectionService
    {
        public void Connect();
        public void Disconnect();
        public void LogIn(string userName, string password);
        public void LoadContent();
        public void LoadResources();
        public void StartListening();
        bool GetRequiredContent();
        bool GetRequiredResources();
        IDataStorage dataStorange { get; set; }
        ISettings settings { get; set; }
        

    }
}
