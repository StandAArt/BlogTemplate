namespace BlogTemplate.Application.Shared.Services.Auth
{
    public interface IUserService
    {
        Task<string?> Authenticate(string email, string password);
        Task<bool> RegisterUser(string email, string password);
    }
}
