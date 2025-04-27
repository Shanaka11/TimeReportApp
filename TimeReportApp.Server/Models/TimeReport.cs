using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class TimeReport
{
    public Guid Id { get; set; }
    public int Hours { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; }

    // Relationships
    [ForeignKey(nameof(Activity))]
    public int ActivityId { get; set; }

}
