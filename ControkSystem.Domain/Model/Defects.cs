using ControkSystem.Domain.Model;

namespace ControkSystem.Domain.Model;

public class Defects
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Priority { get; set; } = null!;
    public Guid UserId { get; set; }
    public virtual User User { get; set;} = null!;
    public string DeadLine { get; set; } = null!;
    public Guid ProjectId { get; set; }
    public string Pictures { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Comm { get; set; }= null!;
    public virtual Projects Project { get; set; } = null!;
}