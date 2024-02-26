
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services
{
    public class Resources : IResources

    {
        public Dictionary<string, string> Icons { get; set; } = new();
        public Dictionary<string, string> Strings { get; set; } = new();

        public string GetIcon(string name)
        {
            return string.Empty;
        }

        public string GetString(string name)
        {
            return string.Empty;
        }

        public async Task LoadContent()
        {
            return;
        }
    }
}
