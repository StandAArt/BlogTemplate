namespace BlogTemplate.Application.Shared.Services.Auth.Dtos
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
