using Auth.Api.Login.Jwt.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Login.Jwt.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                {
                    return BadRequest("Username and password are required.");
                }

                // Authenticate user (example)
                if (!AuthenticateUserAgainstDb(login.Email, login.Password))
                {
                    return Unauthorized("Invalid username or password.");
                }

                // Generate JWT token
                var token = TokenGenerator(name: "To Sebs", email: login.Email);

                // Return token
                return Ok(new UserDto
                {
                    DisplayName = "To Sebs",
                    Email = login.Email,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception was thrown: {ex.Message}");
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            var user = HttpContext.User;
            var email = user.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var name = user.FindFirst("name")?.Value ?? "";

            var result = new UserDto
            {
                Email = email,
                DisplayName = name,
            };
            return result;
        }

        private bool AuthenticateUserAgainstDb(string email, string password)
        {
            // Example authentication logic (replace with your own)
            // Check username and password against a data store (e.g., database)
            // Return true if authentication succeeds, false otherwise
            return email == "true.sebastian@yahoo.com" && password == "asa";
        }

        private string TokenGenerator(string name, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, name),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };

            var secretBytes = Encoding.UTF8.GetBytes(_config["Token:Key"]);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                   _config["Token:Issuer"],
                   _config["Token:Audience"],
                    claims,
                    notBefore: DateTime.Now, //When the token starts to be valid
                    expires: DateTime.Now.AddSeconds(10),
                    signingCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Attach claims principal to current HTTP context
            HttpContext.User = principal;

            return tokenJson;
        }

        //public IActionResult Decode(string part)
        //{
        //    var bytes = Convert.FromBase64String(part);
        //    return Ok(Encoding.UTF8.GetString(bytes));
        //}
    }
}
