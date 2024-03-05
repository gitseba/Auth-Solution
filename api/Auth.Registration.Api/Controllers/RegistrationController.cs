using Auth.Registration.Api.Dtos;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;
using AutoMapper;
using EmailService.Papercut.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

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

            var mapAccount = _mapper.Map<AccountEntity>(registerData);

            var result = _repository.Insert(mapAccount);
            if (result.IsSuccess)
            {
                // Registration successful
                await _emailService.SendAsync("test@test.com", "email verify", MailMessage.GetMailMessage(), true);
                return Ok();//RedirectToAction("Login");
            }
            else
            {
                // If registration fails, add errors to ModelState
                ModelState.AddModelError("", result.ErrorMessage);

                return BadRequest(ModelState);
            }
        }
    }
}
