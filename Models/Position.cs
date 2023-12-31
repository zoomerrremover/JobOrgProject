
using TheJobOrganizationApp.Atributes;

namespace TheJobOrganizationApp.Models;

[Model(DisplayableInTheGlobalSearch = true)]
public class Position:Thing
{
    public Color PosColor { get; set; }

    public int Rank { get; set; }


}
