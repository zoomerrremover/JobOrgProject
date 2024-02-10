
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models.Interfaces;

namespace TheJobOrganizationApp.Models;

[Model(DisplayableInTheGlobalSearch = true)]
public class Worker : Thing,IUser, IHasLocation, IHasContacts , IHasItems , IHasColour 

{


    public string Password { get; set ; }
    public string UserName { get ; set ; }
    public string EmailForLogIn { get ; set ; }
    public string Email { get ; set ; } = string.Empty;
    public string Location { get ; set ; }
    public string PhoneNumber { get ; set ; }= string.Empty;
    public string Buissness { get ; set ; } = string.Empty;
    public List<Item> Items { get; set; } = new();
    public Color Color { get; set; } = Colors.Red;
    public bool IsPicked { get; set; } = false;
    public Position Position { get; set; }
    public float Latitude { get; set; } = 0;
    public float Longitude { get; set; } = 0;

}
