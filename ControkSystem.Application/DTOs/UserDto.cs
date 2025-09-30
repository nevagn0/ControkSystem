namespace ControkSystem.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string Type { get; set; } = null!;
}

public class CreateUserDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Type { get; set; } = null!;
}