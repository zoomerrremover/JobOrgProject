
using System.Collections.ObjectModel;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class APITemp : IAPIService
    {
        public APITemp(IDataStorage dataStorange,FakeDataFactory fakeDataFactory,int numberOfGeneratedData = 10)
        {
            this.dataStorange = dataStorange;
            FakeDataFactory = fakeDataFactory;
            this.numberOfGeneratedData = numberOfGeneratedData;
        }
        int numberOfGeneratedData;

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void LogIn(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public void LoadContent(int VisibilityLevel)
        {
            var localItems = FakeDataFactory.ItemGenerator.Generate(numberOfGeneratedData);
            var localPos = FakeDataFactory.PositionGenerator.Generate(numberOfGeneratedData);
            var localPlaces = FakeDataFactory.PlaceGenerator.Generate(numberOfGeneratedData);
            var localWorkers = FakeDataFactory.WorkerGenerator.Generate(numberOfGeneratedData);
            var localJobs = FakeDataFactory.JobGenerator.Generate(numberOfGeneratedData);
            var localTasks = localJobs
                .SelectMany(job => job.Tasks)
                .ToList();
            var localConts = localJobs
                .Select(job => job.Contractor)
                .ToList();
            dataStorange.AddThing(localPlaces.ToObservableCollection());
            dataStorange.AddThing(localJobs.ToObservableCollection());
            dataStorange.AddThing(localWorkers.ToObservableCollection());
            dataStorange.AddThing(localTasks.ToObservableCollection());
            dataStorange.AddThing(localItems.ToObservableCollection());
            dataStorange.AddThing(localConts.ToObservableCollection());
            dataStorange.AddThing(localPos.ToObservableCollection());

        }

        public FakeDataFactory FakeDataFactory { get; set; }
        public IDataStorage dataStorange { get; set; }
    }
}
