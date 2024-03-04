using Auth.Registration.Api.Dtos;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Registration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRepository<AccountEntity> _repository;
        private readonly IMapper _mapper;

        public RegistrationController(
            IRepository<AccountEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult Register([FromBody] RegisterDto registerData)
        {
            // BE Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // BCrypt.NET or System.Security.Cryptography to securely hash passwords

            var mapAccount = _mapper.Map<AccountEntity>(registerData);

            _repository.Insert(mapAccount);
            //if (result.Succeeded)
            //{
            //    // Registration successful
            //    return RedirectToAction("Login");
            //}
            //else
            //{
            //    // If registration fails, add errors to ModelState
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //    }
                return BadRequest(ModelState);
            //}
        }
    }
}
