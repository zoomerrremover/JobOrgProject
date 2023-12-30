
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class ContentLoader : ILoadableContent

    {

        public ContentLoader()
        {
            SettedCollors = new();
            SettedCollors = new();
        }
        

        public Dictionary<string, Color> SettedCollors { get; set; }
        public Dictionary<string, Dictionary<string, string>> SettedText { get; set; }

  
    }
}
