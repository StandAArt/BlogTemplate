using BlogTemplate.Application.Shared.Services.Auth;
using BlogTemplate.Application.Shared.Services.Auth.Dtos;
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid data." });

            var result = await _userService.RegisterUser(model);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully." });
            }
            else
            {
                // Return the errors if registration failed
                return BadRequest(new { message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }
        }
    }
}

