
namespace TheJobOrganizationApp.Models;

public class Worker : IUser, IHasLocation, IHasContacts , IHasItems

{
    public string Password { get; set ; }
    public string UserName { get ; set ; }
    public string EmailForLogIn { get ; set ; }
    public string Email { get ; set ; }
    public string Location { get ; set ; }
    public string PhoneNumber { get ; set ; }
    public string Buissness { get ; set ; }
    public List<Item> Items { get ; set ; }
}
