namespace TheJobOrganizationApp.Services
{
    public interface ILoadableContent
    {
        public Dictionary<string,Color> SettedCollors { get; set; }

        public Dictionary<string,Dictionary<string,string>> SettedText { get; set; }

        public List<string> InteractableModels { get; set; }

        public void RegisterModels(List<string> models)
        {
        }
        public void RegisterColors(List<string> colors)
        {
        }
        public void RegisterText(List<string> colors)
        {
        }


    }
}
