using System.Data.SqlTypes;
using ControkSystem.Domain.Model;

namespace ControkSystem.Domain.Model;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string HashPassword { get; set; } = null!;
    public string Type { get; set; } = null!;
    public ICollection<UserProject> UserProjects { get; set; }

    public User(){}
    public User(string login, string hashPassword, string type)
    {
        Login = login;
        HashPassword = hashPassword;
        Type = type;
    }
    
}