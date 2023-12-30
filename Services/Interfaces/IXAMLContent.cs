
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IXAMLContent
    {
        public Dictionary<Type, DataTemplate> DataTemplates {get;}
        public Dictionary<Type, ContentPage> ContentPages {get;}
        public DataTemplate DataTemplatesDefaultValue {get;}
        public ContentPage ContentPageDefaultValue {get;}
    }
}
