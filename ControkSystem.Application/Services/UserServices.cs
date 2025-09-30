using ControkSystem.Domain.Model;
using ControkSystem.Domain.Interfaces.Repositories;
using ControkSystem.Application.DTOs;
namespace ControkSystem.Application.Services;

public class UserServices
{
    private readonly IUserRepository _userRepository;
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        var user = new User
        {
            Login = userDto.Login,
            HashPassword = HashPassword(userDto.Password),
            Type = userDto.Type
        };
        
        await _userRepository.AddAsync(user);
        
        return new UserDto
        {
            Id = user.Id,
            Login = user.Login,
            Type = user.Type
        };
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}