
using TheJobOrganizationApp.Models.ModelsProxies;
using TheJobOrganizationApp.ViewModels;

namespace TheJobOrganizationApp.Services
{
    public class Initializator

    {
        public List<Iintializable>Services { get; } = new();

        public Initializator(GlobalSettings st,IDataStorage storage)
        {
            Services.Add(st);
            Services.Add(storage);
            WorkerProxy.Initialize(storage);
        }

        public void Initialize()
        {
            Services.ForEach(i => i.Initialize());

        }
    }
}
