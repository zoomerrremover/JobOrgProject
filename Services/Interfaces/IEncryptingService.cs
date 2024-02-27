
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IEncryptingService
    {
        public string Decrypt(byte[] data);
        public string Decrypt(string data);
        public byte[] Encrypt(byte[] data);
        public string Encrypt(string data);


    }
}
