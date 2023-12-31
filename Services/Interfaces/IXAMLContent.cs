
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IXAMLContent
    {
        public Dictionary<Type, Type> DataTemplates {get;}
        public Dictionary<Type, Type> ContentPages {get;}
        public Type DataTemplatesDefaultValue {get;}
        public Type ContentPageDefaultValue {get;}
    }
}
