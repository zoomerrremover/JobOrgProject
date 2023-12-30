using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.ViewModels.ModelsProxies
{
    public class ThingItemMerge
    {
        public Thing thing { get; set; }

        public Item item { get; set; }

        public ThingItemMerge(Thing thing,Item item)
        {
            this.thing = thing;
            this.item = item;
        }
    }
}
