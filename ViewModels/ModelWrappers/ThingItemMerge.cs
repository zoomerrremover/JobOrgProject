using TheJobOrganizationApp.Models;
namespace TheJobOrganizationApp.ViewModels.ModelWrappers;

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

