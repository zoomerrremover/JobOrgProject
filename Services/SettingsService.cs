
namespace TheJobOrganizationApp.Services
{
    public class TempSettingsService : ISettingsService
    {

        public SettingsService()
        {
             
        }

        public Dictionary<string, Color> SettedCollors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<string, Dictionary<string, string>> SettedText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Dictionary<string, Type> Models { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
