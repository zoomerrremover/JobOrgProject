
namespace TheJobOrganizationApp.Models
{
    [Model(DisplayableInTheGlobalSearch = true)]
    public class Contractor : Thing, IHasContacts
    {
        public string PhoneNumber { get ; set ; }
        public string Email { get ; set ; }
        public string Buissness { get ; set ; }
        public List<Job> Jobs { get; set; }

    }
}
