
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
            ItemGenerator = new Faker<Item>()
                .
            WorkerGenerator = new Faker<Worker>()
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.UserName, (f, m) => m.Name.ToLower().Replace(" ", ""))
                .RuleFor(m => m.Email, (f, m) => $"{m.UserName}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.EmailForLogIn, (f, m) => m.Email)
                .RuleFor(m => m.Password, f => f.Random.Word());


        }
    }
}
