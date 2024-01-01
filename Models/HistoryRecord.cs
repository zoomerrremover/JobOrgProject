
using TheJobOrganizationApp.Atributes;
using TheJobOrganizationApp.Models.Interfaces;
using TheJobOrganizationApp.Models.Misc;

namespace TheJobOrganizationApp.Models;

[Model(DisplayableInTheGlobalSearch =false)] 
public record HistoryRecord
{
    public Guid ID { get; set; }

    public IUser User { get; set; }
    public Thing Subject {  get; set; }
    public HistoryActionType ActionType { get; set; }
    public DateTime Time {  get; set; }
    public string FieldName {  get; set; }
    public string Value { get; set; }
    public string Value2 { get; set; }
    public HistoryRecord()
    {
        ID = Guid.NewGuid();
        Time = DateTime.UtcNow;
    }
    public override string ToString()
    {
        switch (ActionType)
        {
            case HistoryActionType.Added:
                return $"{User.UserName} has created {Subject}.";
            case HistoryActionType.Deleted:
                return $"{User.UserName} has deleted {Subject}.";
            case HistoryActionType.Changed:
                return $"{User.UserName} has changed {FieldName} from {Value} to {Value2}.";
            default:
                return "Undisplayable history event";
        }
    }

}
