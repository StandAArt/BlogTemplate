using BlogTemplate.Application.Shared.Services.Auth;
using BlogTemplate.Application.Shared.Services.Auth.Dtos;
using BlogTemplate.Shared.Constants;
using BlogTemplate.Shared.Constants.User;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpPost(UserConsts.Authenticate)]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var token = await _userService.Authenticate(loginDto.Email, loginDto.Password);
            if (token == null)
                return Unauthorized(UserConsts.InvalidCredentials);

            return Ok(new { Token = token });
        }

        [HttpPost(UserConsts.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (model == null)
                return BadRequest(new { message = GlobalConsts.InvalidData });

            var result = await _userService.RegisterUser(model);

            if (result.Succeeded)
            {
                return Ok(new { message = UserConsts.SuccessRegistration });
            }
            else
            {
                return BadRequest(new { message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }
        }
    }
}

