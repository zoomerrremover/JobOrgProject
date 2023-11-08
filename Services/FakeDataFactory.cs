
using Bogus;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class FakeDataFactory
    {
        Faker<Worker> WorkerGenerator { get; set; }
        Faker<Item> ItemGenerator { get; set; }
        Faker<Place> PlaceGenerator { get; set; }
        Faker<JOTask> TaskGenerator { get; set; }

        Faker<Job> JobGenerator { get; set; }
        Faker<Contractor> ContractorGenerator { get; set; }

        public List<Worker> GenerateWorkers(int numOfWorkers)
        {
            List<Worker> workers = new List<Worker>();
            for(int i = 0; i < numOfWorkers; i++)
            {
                workers.Append(WorkerGenerator.Generate());
            }
            return workers;
        }
        public List<Item> GenerateItems(int numOfItems)
        {
            List<Item> Items = new List<Item>();
            for (int i = 0; i < numOfItems; i++)
            {
                Items.Append(ItemGenerator.Generate());
            }
            return Items;
        }
        public List<Job> GenerateJobs(int numOfJobs)
        {
            List<Job> Jobs = new List<Job>();
            for (int i = 0; i < numOfJobs; i++)
            {
                Jobs.Append(JobGenerator.Generate());
            }
            return Jobs;
        }
        public List<JOTask> GenerateTasks(int num)
        {
            List<JOTask> tasks = new List<JOTask>();
            for (int i = 0; i < num; i++)
            {
                tasks.Append(TaskGenerator.Generate());
            }
            return tasks;
        }
        public FakeDataFactory() {
            WorkerGenerator = new Faker<Worker>()
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.UserName, (f, m) => m.Name.ToLower().Replace(" ", ""))
                .RuleFor(m => m.Email, (f, m) => $"{m.UserName}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.EmailForLogIn, (f, m) => m.Email)
                .RuleFor(m => m.Password, f => f.Random.Word())
                .RuleFor(m => m.Location, f => f.Address.FullAddress())
                .RuleFor(m => m.Items, f => GenerateItems(f.Random.Int(0, 20)));
            ItemGenerator = new Faker<Item>()
                .RuleFor(m => m.Name, f => f.Commerce.Product())
                .RuleFor(m => m.UnitsName, "units")
                .RuleFor(m => m.Qty, f => f.Random.Float(0, 100));
            PlaceGenerator = new Faker<Place>()
                .RuleFor(m => m.Name, f => f.Address.FullAddress())
                .RuleFor(m => m.Location, (f, m) => m.Name);
            TaskGenerator = new Faker<JOTask>()
                .RuleFor(m => m.Name, f => f.Random.Words(4))
                .RuleFor(m => m.Description, f => f.Random.Words(30))
                .RuleFor(m => m.StartTime, f => f.Date.Recent())
                .RuleFor(m => m.FinishTime, f => f.Date.Soon())
                .RuleFor(m => m.Place, PlaceGenerator.Generate())
                .RuleFor(m => m.Workers,f => GenerateWorkers(f.Random.Int(0,5)));
            JobGenerator = new Faker<Job>()
                .RuleFor(m => m.Name, f => f.Random.Words(4))
                .RuleFor(m => m.Description, f => f.Random.Words(30))
                .RuleFor(m => m.StartTime, f => f.Date.Recent())
                .RuleFor(m => m.FinishTime, f => f.Date.Soon())
                .RuleFor(m => m.Tasks, f => GenerateTasks(f.Random.Int(0, 5)))
                .RuleFor(m => m.Contractor, ContractorGenerator.Generate());
            ContractorGenerator = new Faker<Contractor>()
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.Email, (f, m) => $"{m.Name.ToLower().Replace(" ","")}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber());
        }
    }
}