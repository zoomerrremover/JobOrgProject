


using System.Drawing;
using Color = Microsoft.Maui.Graphics.Color;

namespace TheJobOrganizationApp.Models
{
    public class ItemUIL
    {   
        public Item Item { get; set; }

        public static explicit operator ItemUIL(Item item)
        {
            return new ItemUIL {Item=item};
        }

}

    }

