using Auth.Api.Login.Jwt.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                if (!AuthenticateUser(login.Email, login.Password))
                {
                    return Unauthorized("Invalid username or password.");
                }

                // Generate JWT token
                var token = AuthenticateAsync();

                // Return token
                return Ok(new { token });
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

        private bool AuthenticateUser(string email, string password)
        {
            // Example authentication logic (replace with your own)
            // Check username and password against a data store (e.g., database)
            // Return true if authentication succeeds, false otherwise
            return email == "true.sebastian@yahoo.com" && password == "asa";
        }

        private async Task<string> AuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("sebs", "cookie")
            };

            var secretBytes = Encoding.UTF8.GetBytes("not_too_short_secret_otherwise_it_might_error");
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                   "https://localhost:44363/",
                   "https://localhost:44363/",
                    claims,
                    notBefore: DateTime.Now, //When the token starts to be valid
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            //await HttpContext.SignInAsync(principal);

            return tokenJson;
        }

        //public IActionResult Decode(string part)
        //{
        //    var bytes = Convert.FromBase64String(part);
        //    return Ok(Encoding.UTF8.GetString(bytes));
        //}
    }
}
