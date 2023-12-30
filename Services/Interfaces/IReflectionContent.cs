
namespace TheJobOrganizationApp.Services.Interfaces
{
    public interface IReflectionContent
    {
        public List<Type> Models { get;}
        public Dictionary<Type, Type> DetailsViewModels { get;}
    }
}
