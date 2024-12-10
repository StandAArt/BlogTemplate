using BlogTemplate.Application.Shared.Services.Auth.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BlogTemplate.Application.Shared.Services.Auth
{
    public interface IUserService
    {
        Task<string?> Authenticate(string email, string password);
        Task<IdentityResult> RegisterUser(RegisterDto model);
    }
}
