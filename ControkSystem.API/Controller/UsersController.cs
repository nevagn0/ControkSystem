using Microsoft.AspNetCore.Mvc;

namespace ControkSystem.API.Controller;
using ControkSystem.Application.DTOs;
using ControkSystem.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserServices _userServices;

    public UsersController(UserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userdto)
    {
        var user = await _userServices.CreateUserAsync(userdto);
        return Ok(user);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<UserDto>>> GetUsers(Guid id)
    {
        var user = await _userServices.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<UserDto>> DeleteUser(Guid id)
    {
        var deleted = await _userServices.DeleteUserById(id);
        if (!deleted)
            return NotFound("Пользователь не найден");
        
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userServices.GetAllAsync();
        return Ok(users);
    }
}