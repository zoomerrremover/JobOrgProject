
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IInitializator
    {
        public void Initiate();
     
        public bool IsInitializing { get; }
    }
}
