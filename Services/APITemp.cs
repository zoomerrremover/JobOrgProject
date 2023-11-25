
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class APITemp : IAPIService
    {
        public APITemp(IDataStorage dataStorange,FakeDataFactory fakeDataFactory,int numberOfGeneratedData = 1)
        {
            this.dataStorange = dataStorange;
            FakeDataFactory = fakeDataFactory;
            Initiate(numberOfGeneratedData);
        }
        static ObservableCollection<T> ToObservableCol<T>( IEnumerable<T> listToFillFrom)
        {
            var listToFill = new ObservableCollection<T>();
            foreach (T thing in listToFillFrom)
            {
                listToFill.Add(thing);
            }
            return listToFill;
        }

        public void Initiate(int generations)
        {
            var localJobs = FakeDataFactory.JobGenerator.Generate(generations);
            var localWorkers = localJobs
                .SelectMany(job => job.Tasks)
                .SelectMany(task => task.Workers)
                .ToList();
            var localTasks = localJobs
                .SelectMany(job => job.Tasks)
                .ToList();
            var localPlaces = localJobs
                .SelectMany(job => job.Tasks)
                .Select(task => task.Place)
                .ToList();
            var localItems = localWorkers
                .SelectMany(worker => worker.Items)
                .ToList();
            var localConts = localJobs
                .Select(job => job.Contractor)
                .ToList();
            dataStorange.AddThing(ToObservableCol(localPlaces));
            dataStorange.AddThing(ToObservableCol(localJobs));
            dataStorange.AddThing(ToObservableCol(localWorkers));
            dataStorange.AddThing(ToObservableCol(localTasks));
            dataStorange.AddThing(ToObservableCol(localItems));
            dataStorange.AddThing(ToObservableCol(localConts));

        }

        public FakeDataFactory FakeDataFactory { get; set; }
        public IDataStorage dataStorange { get; set; }
    }
}
