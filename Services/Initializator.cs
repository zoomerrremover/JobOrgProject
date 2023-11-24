using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.Services
{
    public class Initializator

    {
        ILoadableContent loadableContent;
        public Initializator(ILoadableContent loadableContent)
        {
            this.loadableContent = loadableContent;
            Initialize();
        }
        public void Initialize()
        {
            loadableContent.InteractableModels = new() { nameof(Worker), nameof(Place),
            nameof(Contractor),nameof(Job),nameof(Item),nameof(Assignment)};
        }
    }
}
