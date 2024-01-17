
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace TheJobOrganizationApp.Models.ModelsProxies
{
    [Proxy(ClassLinked = typeof(Contractor))]
    public partial class ContractorProxy:ThingProxy
    {
        new public Contractor BindingObject { get; set; }
        // CTORS
        //------------------------------------------------------------------------------------------------
        public new static ModelView CreateFromTheModel(Thing model)
        {
            if (model is Contractor)
            {
                var wm = new ContractorProxy(model as Contractor);
                return wm;
            }
            else return null;
        }
        public ContractorProxy(Contractor item) : base(item)
        {
            BindingObject = item;
            Initialize();
        }
        void Initialize()
        {
            queryService.SubscribeForUpdates(InitializeJobs, typeof(Job));
            InitializeJobs();
        }
        private void InitializeJobs(object sender, NotifyCollectionChangedEventArgs e)
        {
            InitializeJobs();
        }
        private void InitializeJobs()
        {
            Jobs.AddRange(from job in queryService.GetItems<Job>()
                          where job.Contractor == BindingObject
                          select job);
        }

        //------------------------------------------------------------------------------------------------
        [ObservableProperty]
        List<Job> jobs;
    }
}
