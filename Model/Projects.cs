using ControkSystem.Models;

namespace ControkSystem.Model;

public class Projects
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Progres { get; set; } = null!;
    public ICollection<Defects> Defects { get; set; }
    public ICollection<UserProject> UserProjects { get; set; } = null!;
}