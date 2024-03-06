using Auth.Registration.Api.Dtos;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;
using AutoMapper;
using EmailService.Papercut.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Auth.Registration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRepository<AccountEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public RegistrationController(
            IRepository<AccountEntity> repository,
            IMapper mapper,
            IEmailService emailService)
        {
            _repository = repository;
            _mapper = mapper;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost()]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerData)
        {
            // BE Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // BCrypt.NET or System.Security.Cryptography to securely hash passwords

            try
            {
                var mapAccount = _mapper.Map<AccountEntity>(registerData);

                // !!! Open Papercut app for email service to work.
                //await _emailService.SendAsync("test@test.com", "email verify", MailMessage.GetMailMessage(), true);
                var result = _repository.Insert(mapAccount);
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                // Return an internal server error response with the error message
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("checkEmail")]
        public async Task<IActionResult> CheckExistingUserAsync([FromQuery] string email)
        {
            try 
            { 
                var result = _repository.Get((account) => account.Email == email);
                if (result != null && result.Count() != 0)
                {
                    return StatusCode(409, "Email already exists.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                // Return an internal server error response with the error message
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
