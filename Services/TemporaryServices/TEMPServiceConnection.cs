using TheJobOrganizationApp.Models;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Models.UtilityClasses;

namespace TheJobOrganizationApp.Services.TemporaryServices
{
    public class TEMPServiceConnection : IServerConnectionService
    {
        FakeDataFactory FakeDataFactory { get; set; }
        IDataStorage dataStorange { get; set; }
        ISettings settings { get; set; }

        IUserController userController { get; set; }

        public bool IsConnected => throw new NotImplementedException();

        public List<Worker> Workers;

        public List<Place> Places;

        public List<Assignment> Assignments;

        public List<Job> Jobs;

        public List<Position> Positions;

        public List<Contractor> Contractors;

        public List<Item> Items;

        int numberOfGeneratedData;
        public TEMPServiceConnection(IUserController userController,IReflectionContent refContent,IDataStorage dataStorange, FakeDataFactory fakeDataFactory, int numberOfGeneratedData = 10)
        {
            this.userController = userController;
            this.dataStorange = dataStorange;
            FakeDataFactory = fakeDataFactory;
            this.numberOfGeneratedData = numberOfGeneratedData;
            Items = FakeDataFactory.ItemGenerator.Generate(numberOfGeneratedData);
            Positions = FakeDataFactory.PositionGenerator.Generate(numberOfGeneratedData);
            Places = FakeDataFactory.PlaceGenerator.Generate(numberOfGeneratedData);
            Workers = FakeDataFactory.WorkerGenerator.Generate(numberOfGeneratedData);
            Jobs = FakeDataFactory.JobGenerator.Generate(numberOfGeneratedData);
            Assignments = Jobs
                .SelectMany(job => job.Tasks)
                .ToList();
            Contractors = Jobs
                .Select(job => job.Contractor)
                .ToList();
            var user = Workers.First();
            user.UserName = "admin";
            user.Password = "123123";
            List<Rule> rules = new List<Rule>();
            var permissions = new List<RuleType> { RuleType.Delete, RuleType.Create, RuleType.Edit };
            foreach (var model in refContent.Models)
            {
                var rule = new Rule() { Model = model, Status = permissions };
                rules.Add(rule);
            }
            Position position = new Position() { Name = "Admin", Permissions = rules };
            dataStorange.AddThing(position);
            user.Position = position;
        }
        public async Task<bool> ConnectAsync()
        {
            return true;
        }

        public async Task<bool> GetAssembly(string version)
        {
            return true;
        }

        public async Task<bool> LoadData()
        {
            dataStorange.AddThing(Places.ToObservableCollection());
            dataStorange.AddThing(Jobs.ToObservableCollection());
            dataStorange.AddThing(Workers.ToObservableCollection());
            dataStorange.AddThing(Assignments.ToObservableCollection());
            dataStorange.AddThing(Items.ToObservableCollection());
            dataStorange.AddThing(Contractors.ToObservableCollection());
            dataStorange.AddThing(Positions.ToObservableCollection());

            return true;
        }

        public async Task<bool> LoadResources()
        {
            return true;
        }

        public async Task<bool> ProfileLogIn(string userName, string password)
        {
            var locWorker = Workers.Where(w => w.UserName == userName);
            if (locWorker.Count() > 0)
            {
                var Worker = locWorker.FirstOrDefault();
                if(Worker.Password == password)
                {
                    userController.SetPermissions(Worker);
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ServerLogIn(string serverId)
        {
            return true;
        }

        public async Task StartListening()
        {
            return;
        }

        public Task<bool> ProfileLogOut()
        {
            throw new NotImplementedException();
        }
    }
}
