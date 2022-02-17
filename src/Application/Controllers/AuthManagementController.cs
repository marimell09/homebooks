using Domain.Dtos;
using Domain.Dtos.Token;
using Domain.Dtos.User;
using Domain.Interfaces.Exceptions;
using Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")] // api/AuthManagement
    [ApiController]
    public class AuthManagementController : ControllerBase
    {

        private IAuthService _service;
        private ILogger<AuthManagementController> _logger;

        public AuthManagementController(IAuthService service, ILogger<AuthManagementController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var jwtToken = await _service.Register(user);
                return Ok(jwtToken);
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var jwtToken = await _service.Login(user);
                return Ok(jwtToken);
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.RefreshToken(tokenRequest);
                return Ok(result);
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUsers();
            return Ok(users);
        }

    }
}
