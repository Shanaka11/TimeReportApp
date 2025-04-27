using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey(nameof(Project))]
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    virtual public ICollection<TimeReport> TimeReports { get; set; }
}
