using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ControkSystem.Domain.Model;
using ControkSystem.Application.DTOs;
using ControkSystem.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ControkSystem.Application.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
    
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        private static class UserTypes
        {
            public const string Engineer = "Инженер";
            public const string Manager = "Менеджер";
            public const string Observer = "Наблюдатель";
        }
        
        public AuthService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByLoginAsync(request.Login);
            if (existingUser != null)
                throw new ArgumentException("Пользователь с таким логином уже существует");
            
            var RoleUser = new[] {UserTypes.Engineer, UserTypes.Manager, UserTypes.Observer};
            if (!RoleUser.Contains(request.Type))
                throw new ArgumentException($"Недопустимая роль '{request.Type}'. Допустимые роли: {string.Join(", ", RoleUser)}");
            
            var user = new User(
                request.Login,
                HashPassword(request.Password),
                request.Type
            );

            await _userRepository.AddAsync(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByLoginAsync(request.Login);
            if (user == null)
                throw new ArgumentException("Неверный логин или пароль");

            var passwordValid = VerifyPassword(request.Password, user.HashPassword);
            if (!passwordValid)
                throw new ArgumentException("Неверный логин или пароль");
            
            var authresponse = GenerateJwtToken(user);
            
            
            
            return authresponse;
        }

        private AuthResponse GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim("Type", user.Type),
                new Claim(ClaimTypes.Role, user.Type),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expires,
                User = new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    Type = user.Type
                }
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<bool> VerifyPasswordAsync(string login, string password)
        {
            var user = await _userRepository.GetByLoginAsync(login);
            if (user == null)
                return false;

            return VerifyPassword(password, user.HashPassword);
        }
        
        private bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}