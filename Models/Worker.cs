
namespace TheJobOrganizationApp.Models;

public class Worker : IUser, IHasLocation, IHasContacts , IHasItems

{
    public string password { get; set ; }
    public string username { get ; set ; }
    public string email { get ; set ; }
    public string Email { get ; set ; }
    public string Location { get ; set ; }
    public string PhoneNumber { get ; set ; }
    public string Buissness { get ; set ; }
    public List<Item> Items { get ; set ; }
}
