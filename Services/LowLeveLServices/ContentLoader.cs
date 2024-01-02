
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;

namespace TheJobOrganizationApp.Services
{
    public class Resources : IResources

    {
        public Dictionary<string, string> Icons => throw new NotImplementedException();

        public Dictionary<string, string> Strings => throw new NotImplementedException();

        public Dictionary<string, GradientBrush> Colors => throw new NotImplementedException();

        public bool CheckResources(List<string> RequiredContent)
        {
            throw new NotImplementedException();
        }

        public string GetIcon(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(string name)
        {
            throw new NotImplementedException();
        }

        public void LoadContent()
        {
            throw new NotImplementedException();
        }
    }
}
