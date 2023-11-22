namespace TheJobOrganizationApp.Services
{
    public interface ISettingsService
    {
        public Dictionary<string,Color> SettedCollors { get; set; }

        public Dictionary<string,Dictionary<string,string>> SettedText { get; set; }

        public Dictionary<string,Type> Models { get; set; }

    }
}
