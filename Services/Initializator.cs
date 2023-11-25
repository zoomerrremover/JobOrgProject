using Foundation;
using System.Reflection;
using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.Services
{
    public class Initializator

    {
        ILoadableContent loadableContent;

        IDataStorage storage;

        SharedControls sharedControls;

        GlobalSettings globalSettings;
        public Initializator(GlobalSettings settings,SharedControls controls,ILoadableContent loadableContent,IDataStorage dataStorage)
        {
            globalSettings = settings;
            storage = dataStorage;
            this.loadableContent = loadableContent;
            this.sharedControls = controls;
            Initialize();
        }
        public void InitializeDatabase()
        {
            foreach(var model in globalSettings.Models)
            {
                storage.RegisterModel(model);
            }
            
        }
        public bool InitializeModels()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && Attribute.IsDefined(type, typeof(Model))){

                    var attribute = (Model)Attribute.GetCustomAttribute(type, typeof(Model));

                    globalSettings.Models.Add(type);
                    // If IsActive is true, add the type to the list
                    if (attribute.DisplayableInTheGlobalSearch)
                    {
                        sharedControls.GSDisplayableModels.Add(type.Name);
                    }
                }
            }
            return true;

        }
        public void Initialize()
        {
            loadableContent.InteractableModels = new() { nameof(Worker), nameof(Place),
            nameof(Contractor),nameof(Job),nameof(Item),nameof(Assignment)};
        }
    }
}
