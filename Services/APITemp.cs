
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
            var localItems = FakeDataFactory.ItemGenerator.Generate(generations);
            var localPos = FakeDataFactory.PositionGenerator.Generate(generations);
            var localPlaces = FakeDataFactory.PlaceGenerator.Generate(generations);
            var localWorkers = FakeDataFactory.WorkerGenerator.Generate(generations);
            var localJobs = FakeDataFactory.JobGenerator.Generate(generations);
            var localTasks = localJobs
                .SelectMany(job => job.Tasks)
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
            dataStorange.AddThing(ToObservableCol(localPos));

        }

        public FakeDataFactory FakeDataFactory { get; set; }
        public IDataStorage dataStorange { get; set; }
    }
}
