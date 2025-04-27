namespace server.Models;

public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    virtual public ICollection<Project> Projects { get; set; }
}
