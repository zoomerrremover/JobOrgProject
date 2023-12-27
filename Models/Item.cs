
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Item:Thing
    {
        public float Qty { get; set; }
        public string UnitsName { get; set; }

        public float Price { get; set; }
    }
}
