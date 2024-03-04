namespace TheJobOrganizationApp.Models.UtilityClasses;

public class Rule
{
    public Type Model { get; set; }
    public List<RuleType> Status { get; set; } = new();
}
