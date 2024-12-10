using BlogTemplate.Application.Shared.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var token = await _userService.Authenticate(loginDto.Email, loginDto.Password);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { Token = token });
        }
    }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}