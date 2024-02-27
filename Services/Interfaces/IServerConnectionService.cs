namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IServerConnectionService
    {
        public bool IsConnected {  get; }
        public Task<bool> ConnectAsync();
        public Task<bool> ServerLogIn(string serverId);
        public Task<bool> ProfileLogIn(string userName, string password);
        public Task<bool> LoadData();

        public Task<bool> GetAssembly(string version);
        public Task<bool> LoadResources();
        public Task StartListening();
    }
}
