
namespace TheJobOrganizationApp.Models
{
    public class Place : Thing, IHasLocation, IHasItems
    {
        public string Location { get ; set ; }
        public List<Item> Items { get ; set ; }
    }
}
