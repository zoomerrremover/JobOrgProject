
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IEncryptingService
    {
        public string Decrypt(byte[] data);

        public byte[] Encrypt(string data);


    }
}
