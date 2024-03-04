using Auth.Api.Cookie.Dtos;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.Api.Cookie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepository<AccountEntity> _repository;

        public AccountController(IRepository<AccountEntity> repository)
        {
            _repository = repository;
        }



        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto registerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // BCrypt.NET or System.Security.Cryptography to securely hash passwords

            var account = new AccountEntity {
                Email = registerData.Email, 
                Password = registerData.Password 
            };

            _repository.Insert(account);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginData)
        {
            var accounts = _repository.Get();
            if (accounts.Any(a => a.Email == loginData.Email))
            {
                await LoginCookie("api/private", accounts?.FirstOrDefault()?.Email);
                return Ok();
            }

            return NotFound();
        }

        private async Task<IActionResult> LoginCookie(string ReturnUrl, string email)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Sebs.Cookie"),
                    new Claim(ClaimTypes.NameIdentifier, "id-1234"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("FavoriteQuote", "Hasta la Vista")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                //SignInAsync creates an encrypted cookie and adds it to the current response.
                //If AuthenticationScheme isn't specified, the default scheme is used.
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties);

                return LocalRedirect(ReturnUrl);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception was thrown: {ex.Message}");
            }
        }

    }
}
