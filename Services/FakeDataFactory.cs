
using Bogus;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class FakeDataFactory
    {
       public Faker<Worker> WorkerGenerator { get; set; }
       public Faker<Item> ItemGenerator { get; set; }
       public Faker<Place> PlaceGenerator { get; set; }
       public Faker<JOTask> TaskGenerator { get; set; }

       public Faker<Job> JobGenerator { get; set; }
       public Faker<Contractor> ContractorGenerator { get; set; }
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
                .RuleFor(m => m.Color, f=> f.Commerce.Color())
                .RuleFor(m => m.Items, f => ItemGenerator.Generate(f.Random.Int(0, 20)));
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
                .RuleFor(m => m.Place, PlaceGenerator.Generate(1).First())
                .RuleFor(m => m.Workers,f => WorkerGenerator.Generate(f.Random.Int(0,5)));
            ContractorGenerator = new Faker<Contractor>()
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.Email, (f, m) => $"{m.Name.ToLower().Replace(" ", "")}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber());
            JobGenerator = new Faker<Job>()
                .RuleFor(m => m.Name, f => f.Random.Words(4))
                .RuleFor(m => m.Description, f => f.Random.Words(30))
                .RuleFor(m => m.StartTime, f => f.Date.Recent())
                .RuleFor(m => m.FinishTime, f => f.Date.Soon())
                .RuleFor(m => m.Tasks, f => TaskGenerator.Generate(f.Random.Int(0, 5)))
                .RuleFor(m => m.Contractor, ContractorGenerator.Generate(1).First());
        }
    }
}