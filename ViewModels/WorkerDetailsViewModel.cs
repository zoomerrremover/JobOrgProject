
//using CommunityToolkit.Mvvm.ComponentModel;
//using System.Collections.ObjectModel;
//using TheJobOrganizationApp.Models;
//using TheJobOrganizationApp.Services;

//namespace TheJobOrganizationApp.ViewModels
//{
//    [QueryProperty("Worker", "Worker")]
//    public partial class WorkerDetailsViewModel : BaseViewModel, IQueryAttributable
//    {

//        IDataStorage ds;

//        [ObservableProperty]
//        [NotifyPropertyChangedFor(nameof(ObsAssignments))]

//        Worker worker;

//        public ObservableCollection<Assignment> Assignments
//        {
//            get => ds.GetItems<Assignment>();
//        } 

//        public ObservableCollection<Assignment> ObsAssignments
//        {
//            get
//            {
//                var listOfAssignments = Assignments.Where(w => w.Workers.Contains(Worker)).ToList();
//                ObservableCollection<Assignment> assignments = new ObservableCollection<Assignment>();
//                listOfAssignments.ForEach(assignments.Add);
//                return assignments;
//            }
//        }


//        public WorkerDetailsViewModel(IDataStorage ds)
//        {
//            this.ds = ds;       
//        }

//        public void ApplyQueryAttributes(IDictionary<string, object> query)
//        {
//            Worker = (Worker)query.FirstOrDefault().Value;
//        }
//    }
//}
