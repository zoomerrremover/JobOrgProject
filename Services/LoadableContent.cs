
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class LoadableContent : ILoadableContent

    {

        public LoadableContent()
        {
            SettedCollors = new();
            SettedCollors = new();
            InteractableModels = new();
        }
        

        public Dictionary<string, Color> SettedCollors { get; set; }
        public Dictionary<string, Dictionary<string, string>> SettedText { get; set; }
        public List<string> InteractableModels { get; set; }
  
    }
}
