using Auth.Registration.Api.Dtos;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System.Net;

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
        public async Task<ActionResult<RegisteredUserDto>> RegisterAsync([FromBody] RegisterDto registerData)
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
                if (!result.IsSuccess) return BadRequest();

                // Return a 201 Created response with the location header pointing to the newly created resource
                return StatusCode((int)HttpStatusCode.Created, new RegisteredUserDto
                {
                    DisplayName = registerData.Name,
                    Email = registerData.Email
                });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                // Return an internal server error response with the error message
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("checkEmail")]
        public bool CheckEmail([FromQuery] string email)
        {
            try
            {
                var result = _repository.Get((account) => account.Email == email);
                if (result != null && result.Count() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                // Return an internal server error response with the error message
                return false;
            }
        }
    }
}
