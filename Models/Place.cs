
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Place : Thing, IHasLocation, IHasItems
    {
        public string Location { get ; set ; }
        public List<Item> Items { get ; set ; }
    }
}
