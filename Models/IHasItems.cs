namespace TheJobOrganizationApp.Models
{
    public interface IHasItems
    {
        public List<Item> Items { get; set; }
    }
}
