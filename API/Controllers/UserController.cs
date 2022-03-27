using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<IdentityUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest();
            }
            var newUser = new IdentityUser()
            {
                UserName = username,
                Email = email,
            };

            var result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                _logger.LogInformation("Registered user {newUser}", newUser);
                return Ok(result);
            }

            _logger.LogError("Cannot register user {newUser}", newUser);
            return BadRequest(result);
        }
    }
}
