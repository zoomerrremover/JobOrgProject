
namespace TheJobOrganizationApp.Services
{
    public class APITemp : IAPIService
    {
        public APITemp(IDataStorage dataStorange,FakeDataFactory fakeDataFactory)
        {
            this.dataStorange = dataStorange;
            FakeDataFactory = fakeDataFactory;
            Initiate();
        }
        public void Initiate()
        {
            dataStorange.Jobs = FakeDataFactory.GenerateJobs(100);
            dataStorange.Workers = dataStorange.Jobs
                .SelectMany(job => job.Tasks)
                .SelectMany(task => task.Workers)
                .ToList();
            dataStorange.Tasks = dataStorange.Jobs
                .SelectMany(job => job.Tasks)
                .ToList();
            dataStorange.Items = dataStorange.Jobs
                .SelectMany(job => job.Tasks)
                .SelectMany(task => task.Workers)
                .SelectMany(worker => worker.Items)
                .ToList();
            dataStorange.Contractors = dataStorange.Jobs
                .SelectMany(job => job.Contractor)
                .ToList();



        }

        public FakeDataFactory FakeDataFactory { get; set; }
        public IDataStorage dataStorange { get; set; }
    }
}
