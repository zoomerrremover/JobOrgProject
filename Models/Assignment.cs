
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Assignment:TConstrained
    {
        public List<Worker> Workers;

        public List<Color> WorkerColours { get => Workers.Select(x => x.Color).ToList(); }
        
    }
}
