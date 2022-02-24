using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    public class TokenController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public TokenController(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]TokenRequestModel model)
        {
            if (model?.grant_type == null)
                return new StatusCodeResult(500);

            switch (model.grant_type)
            {
                case "password":
                    return await GetToken(model.username,model.password);
                default:
                    return new UnauthorizedResult();
            }
        }

        public async Task<IActionResult> GetToken(string userName, string password)
        {
            if (await IsValidUsernameAndPassword(userName, password))
            {
                return new ObjectResult(await GenerateToken(userName));
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            var user = await GetUser(username);

            return await _userManager.CheckPasswordAsync(user, password);
        }

        private async Task<dynamic> GenerateToken(string username)
        {
            var user = await GetUser(username);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            };

            var tokenExpirationMins = ConfigurationBinder.GetValue<int>(_config, "Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Auth:Jwt:Key"]));
            var now = DateTime.Now;

            var token = new JwtSecurityToken(
                                    issuer: _config["Auth:Jwt:Issuer"],
                                    audience: _config["Auth:Jwt:Audience"],
                                    claims: claims,
                                    notBefore: now,
                                    expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                                    signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                                    );

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = username
            };

            return output;
        }

        private async Task<IdentityUser> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                await _userManager.FindByEmailAsync(username);
            }

            return user;
        }
    }
}
