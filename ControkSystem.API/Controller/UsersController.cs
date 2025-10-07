using Microsoft.AspNetCore.Mvc;

namespace ControkSystem.API.Controller;
using ControkSystem.Application.DTOs;
using ControkSystem.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserServices _userServices;
    private readonly AuthService _authService;

    public UsersController(UserServices userServices,AuthService authService)
    {
        _authService = authService;
        _userServices = userServices;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
           var result = await _authService.LoginAsync(request);
           
           HttpContext.Response.Cookies.Append("access_token", result.Token, new CookieOptions
           {
               Expires = result.Expires,
               HttpOnly = false,
               IsEssential = true,
               Secure = true,
               SameSite = SameSiteMode.None
           });
           
           return NoContent();
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        try
        {
            await _authService.RegisterAsync(request);
            return Ok(new
            {
                message = "Успешный вход"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
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
    [HttpGet("check-cookies")]
    public IActionResult CheckCookies()
    {
        var cookies = Request.Cookies.Keys.ToDictionary(
            key => key, 
            key => Request.Cookies[key] ?? "null"
        );
    
        return Ok(new { 
            cookies = cookies,
            headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
        });
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                Expires = DateTime.UtcNow.AddYears(-1)
            };

            Response.Cookies.Append("access_token", "", cookieOptions);
                
            return Ok(new { 
                message = "Успешный выход"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}