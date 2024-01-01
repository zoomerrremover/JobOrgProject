
namespace TheJobOrganizationApp.Services
{
    public interface IAPIService
    {
        public void Connect();
        public void Disconnect();
        public void LogIn(string userName, string password);
        public void LoadContent(int VisibilityLevel);
        IDataStorage dataStorange { get; set; }
        

    }
}
