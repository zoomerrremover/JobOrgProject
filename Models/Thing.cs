
namespace TheJobOrganizationApp.Models;

public abstract class Thing

{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public List<HistoryRecord> History { get; set; }

    public Thing()
    {
         Id = Guid.NewGuid();
    }
}
