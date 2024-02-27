

using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services.LowLeveLServices
{
    public class EncryptingService : IEncryptingService
    {
        public string Decrypt(byte[] data)
        {
            return String.Empty;
        }

        public string Decrypt(string data)
        {
            return data;
        }

        public byte[] Encrypt(byte[] data)
        {
            return data;
        }

        public string Encrypt(string data)
        {
            return data;
        }

    }
}
