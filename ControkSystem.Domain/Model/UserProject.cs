using ControkSystem.Domain.Model;
namespace ControkSystem.Domain.Model;

public class UserProject
{
    public Guid IdUser { get; set; }
    public Guid IdProject { get; set; }
    public virtual User User { get; set; }
    public virtual Projects Projects { get; set; }
}