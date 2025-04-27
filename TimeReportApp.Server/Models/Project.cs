using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Client client { get; set; }
        virtual public ICollection<Activity> Activities { get; set; }
    }
}
