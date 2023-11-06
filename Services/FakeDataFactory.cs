
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
        Faker<Contractor> ContractorGenerator { get; set; }
        public FakeDataFactory() {
            WorkerGenerator = new Faker<Worker>()
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.UserName, (f, m) => m.Name.ToLower().Replace(" ", ""))
                .RuleFor(m => m.Email, (f, m) => $"{m.UserName}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.EmailForLogIn, (f, m) => m.Email)
                .RuleFor(m => m.Password, f => f.Random.Word())
                .RuleFor(m => m.Location, f => f.Address.FullAddress());
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
                .RuleFor(m => m.FinishTime, f => f.Date.Soon());
            Job





        }
    }
}