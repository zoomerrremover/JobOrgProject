
using Bogus;
using TheJobOrganizationApp.Models;

namespace TheJobOrganizationApp.Services
{
    public class FakeDataFactory
    {
       public Faker<Worker> WorkerGenerator { get; set; }

        public Faker<Position> PositionGenerator { get; set; }
        public Faker<Item> ItemGenerator { get; set; }
       public Faker<Place> PlaceGenerator { get; set; }
       public Faker<Assignment> TaskGenerator { get; set; }

       public Faker<Job> JobGenerator { get; set; }
       public Faker<Contractor> ContractorGenerator { get; set; }

        public List<Item> ItemPool { get; set; } = new();
        public List<Worker> WorkerPool { get; set; } = new();
        public List<Place> PlacePool { get; set; } = new();
        public List<Position> PositionPool { get; set; } = new();

        public FakeDataFactory() {
            PositionGenerator = new Faker<Position>()
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Name, f => f.Commerce.Department())
                .FinishWith((f, t) => PositionPool.Add(t));
            WorkerGenerator = new Faker<Worker>()
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.UserName, (f, m) => m.Name.ToLower().Replace(" ", ""))
                .RuleFor(m => m.Email, (f, m) => $"{m.UserName}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.EmailForLogIn, (f, m) => m.Email)
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Password, f => f.Random.Word())
                .RuleFor(w => w.Position, f => f.PickRandom(PositionPool))
                .RuleFor(m => m.Latitude, f => f.Random.Float(-90,90))
                .RuleFor(m => m.Longitude, f => f.Random.Float(-180, 180))
                .RuleFor(m => m.Color, f => Color.FromRgb(f.Random.Byte(), f.Random.Byte(), f.Random.Byte()))
                .RuleFor(m => m.Items, f =>
                {
                    var newPool = f.PickRandom(ItemPool, f.Random.Int(0, 10))
                                   .ToList();
                    newPool.ForEach(i => i.Qty = f.Random.Float(0, 100));
                    return newPool;
                })
                .FinishWith((f, t) => WorkerPool.Add(t));
            ItemGenerator = new Faker<Item>()
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Name, f => f.Commerce.Product())
                .RuleFor(m => m.UnitsName, "units")
                .RuleFor(m => m.Qty, f => f.Random.Float(0, 100))
                .FinishWith((f, t) => ItemPool.Add(t));
            PlaceGenerator = new Faker<Place>()
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Name, f => f.Address.FullAddress())
                .RuleFor(m => m.Address, (f, m) => m.Name)
                .RuleFor(m => m.Latitude, f => f.Random.Float(-90, 90))
                .RuleFor(m => m.Longitude, f => f.Random.Float(-180, 180))
                .RuleFor(m => m.Items, f =>
                {
                    var newPool = f.PickRandom(ItemPool, f.Random.Int(0, 10))
                                   .ToList();
                    newPool.ForEach(i => i.Qty = f.Random.Float(0, 100));
                    return newPool;
                })
                .FinishWith((f, t) => PlacePool.Add(t));
            TaskGenerator = new Faker<Assignment>()
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Name, f => f.Random.Words(4))
                .RuleFor(m => m.Description, f => f.Random.Words(30))
                .RuleFor(m => m.StartTime, f => f.Date.Recent())
                .RuleFor(m => m.FinishTime, f => f.Date.Soon())
                .RuleFor(m => m.Place, f => f.PickRandom(PlacePool))
                .RuleFor(m => m.Workers,f => f.PickRandom(WorkerPool,f.Random.Number(1,4)).ToList() );
            ContractorGenerator = new Faker<Contractor>()
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Name, f => f.Name.FullName())
                .RuleFor(m => m.Email, (f, m) => $"{m.Name.ToLower().Replace(" ", "")}@gmail.com")
                .RuleFor(m => m.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(m => m.Buissness, f => f.Phone.PhoneNumber());
            JobGenerator = new Faker<Job>()
                .RuleFor(m => m.Description, f => f.Random.Words())
                .RuleFor(m => m.Name, f => f.Random.Words(4))
                .RuleFor(m => m.Description, f => f.Random.Words(30))
                .RuleFor(m => m.StartTime, f => f.Date.Recent())
                .RuleFor(m => m.FinishTime, f => f.Date.Soon())
                .RuleFor(m => m.Tasks, f => TaskGenerator.Generate(f.Random.Int(0, 5)))
                .RuleFor(m => m.Contractor,f=> f.PickRandom(ContractorGenerator.Generate(100)));
        }

    }
}  