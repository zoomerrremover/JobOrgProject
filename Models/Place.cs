
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Place : Thing, IHasLocation, IHasItems
    {
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public List<Item> Items { get ; set ; }
    }
}
