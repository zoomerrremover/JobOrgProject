
using System.Text;
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
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public HistoryRecord()
    {
        ID = Guid.NewGuid();
        Time = DateTime.UtcNow;
    }
    public string Message
    {
        get
        {
            StringBuilder message = new(User.UserName);
            switch (ActionType)
            {
                case HistoryActionType.Added:
                    message.Append($"Created this {Subject.GetType().Name}.");
                    break;
                case HistoryActionType.Deleted:
                    message.Append($" has deleted {Subject.GetType().Name}.");
                    break;
                case HistoryActionType.Changed:
                    message.Append($" has changed {FieldName}");
                    var changPart = OldValue is not null? $"{FieldName} from {OldValue} to {NewValue}.":"";
                    message.Append(changPart);
                    break;
                default:
                    return "Undisplayable history event";
            }
            message.Append($" on {Time.ToString("d")} at {Time.ToString("t")}");
            return message.ToString();
        }
    }

}
