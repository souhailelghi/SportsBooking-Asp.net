using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBookingSystem.Modles.Dto;
using SportsManagementApp.Interfaces;

namespace SportsBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto user)
        {
            var registeredUser = await _userService.RegisterUserAsync(user);
            return Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.LoginUserAsync(model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUserList()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
