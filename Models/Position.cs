
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models.UtilityClasses;

namespace TheJobOrganizationApp.Models;

[Model(DisplayableInTheGlobalSearch = true)]
public class Position:Thing
{
    public Color PosColor { get; set; }

    public int VisibilityLevel { get; set; }

    public List<Rule> Permissions { get; set; } = new();
}
