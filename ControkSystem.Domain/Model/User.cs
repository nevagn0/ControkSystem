using System.Data.SqlTypes;
using ControkSystemm.Domain.Model;

namespace ControkSystemm.Domain.Model;

public class User()
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string HashPassword { get; set; } = null!;
    public string Type { get; set; } = null!;
    public ICollection<UserProject> UserProjects { get; set; }
}