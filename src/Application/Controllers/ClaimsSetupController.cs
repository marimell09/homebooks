using Domain.Entities;
using Domain.Interfaces.Exceptions;
using Domain.Interfaces.User;
using Domain.Security;
using Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClaimsSetupController : ControllerBase
    {

        private IClaimsService _service;
        private ILogger<ClaimsSetupController> _logger;

        public ClaimsSetupController(IClaimsService service, ILogger<ClaimsSetupController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            try
            {
                var userClaims = await _service.GetAllClaims(email);
                return Ok(userClaims);
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.Message);

            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Route("AddClaimsToUser")]
        public async Task<IActionResult> AddClaimsToUser(string email, string claimName, string claimValue)
        {
            try
            {
                var user = await _service.AddClaimsToUser(email, claimName, claimValue);
                return NoContent();
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.Message);

            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
