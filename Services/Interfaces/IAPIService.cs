
namespace TheJobOrganizationApp.Services
{
    public interface IAPIService
    {
        public string Connect() => "0";

        public string Disconnect() => "0";

        IDataStorage dataStorange { get; set; }

        public void Initiate() { }


    }
}
