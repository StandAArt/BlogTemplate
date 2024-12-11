using BlogTemplate.Application.Shared.Services.Auth;
using BlogTemplate.Application.Shared.Services.Auth.Dtos;
using BlogTemplate.Domain.Models;
using BlogTemplate.Shared.Constants.JwtToken;
using BlogTemplate.Shared.Constants.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BlogTemplate.Application.Services.Auth
{
    public class UserService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration) : BaseService, IUserService
    {
        public async Task<string?> Authenticate(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration[$"{JwtTokenConsts.Jwt}:{JwtTokenConsts.Key}"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration[$"{JwtTokenConsts.Jwt}:{JwtTokenConsts.DurationInMinutes}"])),
                    Issuer = _configuration[$"{JwtTokenConsts.Jwt}:{JwtTokenConsts.Issuer}"],
                    Audience = _configuration[$"{JwtTokenConsts.Jwt}:{JwtTokenConsts.Audience}"],
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            return null;
        }

        public async Task<IdentityResult> RegisterUser(RegisterDto model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = UserConsts.UserExistsError });
            }

            var user = Mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
