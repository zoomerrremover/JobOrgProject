namespace TheJobOrganizationApp.Services
{
    public interface ILoadableContent
    {
        public Dictionary<string,Color> SettedCollors { get; set; }

        public Dictionary<string,Dictionary<string,string>> SettedText { get; set; }

        public void RegisterColors(List<string> colors)
        {
        }
        public void RegisterText(List<string> colors)
        {
        }


    }
}
