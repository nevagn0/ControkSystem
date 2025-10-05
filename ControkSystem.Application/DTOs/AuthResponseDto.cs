namespace ControkSystem.Application.DTOs
{
    public class RegisterRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Type { get; set; } = null!;
    }

    public class LoginRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public UserDto User { get; set; } = null!;
    }
}