
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp.ViewModels.ModelsProxies
{
    public class WorkerProxy : Worker
    {
        static IDataStorage queryService;
        
        //ObservableCollection<Assignment> workerAssignments
        //{
        //    get
        //    {
        //        queryService.GetItems<Assignment>().Where(a => a.Workers.Contains(this)).ToList().ForEach();
        //    }
        //}
        public WorkerProxy(IDataStorage qS)
        {
            queryService = qS;
        }
        public WorkerProxy()
        {
            
        }
    }
}
