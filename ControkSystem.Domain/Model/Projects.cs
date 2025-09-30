namespace ControkSystem.Domain.Model;

using ControkSystem.Domain.Model;
public class Projects
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Progres { get; set; } = null!;
    public ICollection<Defects> Defects { get; set; }
    public ICollection<UserProject> UserProjects { get; set; } = null!;
    
    public Projects() {}
    
    public Projects(string name, string progress)
    {
        Name = name;
        Progres = progress;
        CreatedAt = DateTime.UtcNow;
        Defects = new List<Defects>();
        UserProjects = new List<UserProject>();
    }
    
}