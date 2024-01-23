
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models.Interfaces;

namespace TheJobOrganizationApp.Models;

[Model(DisplayableInTheGlobalSearch = true)]
public class Worker : Thing,IUser, IHasLocation, IHasContacts , IHasItems , IHasColour 

{


    public string Password { get; set ; }
    public string UserName { get ; set ; }
    public string EmailForLogIn { get ; set ; }
    public string Email { get ; set ; }
    public string Location { get ; set ; }
    public string PhoneNumber { get ; set ; }
    public string Buissness { get ; set ; }
    public List<Item> Items { get; set; } = new();
    public Color Color { get; set; }
    public bool IsPicked { get; set; }
    public Position Position { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }

}
