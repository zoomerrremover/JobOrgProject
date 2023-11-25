
namespace TheJobOrganizationApp.Models;

public interface IHasContacts
{
    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Buissness { get; set;}
}
