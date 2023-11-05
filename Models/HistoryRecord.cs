
namespace TheJobOrganizationApp.Models;

public record HistoryRecord
{
    public string Subject {  get; set; }

    public string SubjectID { get; set; }

    public string Action { get; set; }

    public DateTime Time {  get; set; }

}
