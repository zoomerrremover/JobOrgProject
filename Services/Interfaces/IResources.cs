
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IResources
    { 
        Dictionary<string,string> Icons { get;}
        Dictionary<string,string> Strings { get;}
        Dictionary<string, GradientBrush> Colors { get; }
        public string GetIcon(string name);
        public string GetString(string name);
        public void LoadContent();
    }
}
