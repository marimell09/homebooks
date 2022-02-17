using Domain.Dtos.Address;
using Domain.Entities;
using Domain.Interfaces.Address;
using Domain.Interfaces.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")] //api/address
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressController : ControllerBase
    {

        private IAddressService _service;
        private ILogger<AddressController> _logger;

        public AddressController(IAddressService service, UserManager<ApplicationUser> userManager, ILogger<AddressController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addresses = await _service.GetAll();
                if (addresses.Any())
                {
                    return Ok(addresses);
                }

                return NotFound();
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
        [Route("userId", Name = "GetAddressesWithUserId")]
        [Authorize(Roles = "Administrator,Customer,Seller")]
        public async Task<ActionResult> GetAddressesByUserId(Guid userId)
        {
            try
            {
                isModelValid();
                isAuthorized(userId);
                var addresses = await _service.FindAddressesByLogin(userId);
                if (addresses.Any())
                {
                    return Ok(addresses);
                }

                return NotFound();
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("id", Name = "GetAddressWithId")]
        [Authorize(Roles = "Administrator,Customer,Seller")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                isModelValid();
                var result = await _service.Get(id);
                if (result == null)
                {
                    return NotFound();

                }
                isAuthorized(result.UserId);
                return Ok(result);
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Customer,Seller")]
        public async Task<ActionResult> Post([FromBody] AddressCreateDto address)
        {
            isModelValid();
            try
            {
                isAuthorized(address.UserId);

                var result = await _service.Post(address);
                if (result == null)
                {
                    return BadRequest();
                }
                return Created(new Uri(Url.Link("GetAddressWithId", new { id = result.Id })), result);
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Customer,Seller")]
        public async Task<ActionResult> Put([FromBody] AddressUpdateDto address)
        {
            try
            {
                isModelValid();
                isAuthorized(address.UserId);
                var result = await _service.Put(address);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);

            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Customer,Seller")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                isModelValid();

                AddressDto address = await _service.Get(id);
                isAuthorized(address.UserId);

                await _service.Delete(id);
                return NoContent();
            }
            catch (ApiException apiExc)
            {
                return StatusCode((int)apiExc.StatusCode, apiExc.newMessage);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private void isModelValid()
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    newMessage = "The user is not allowed to perform this action."
                };
            }
        }

        private void isAuthorized(Guid actionUserId)
        {
            var loggedUserId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (Guid.Parse(loggedUserId) != actionUserId)
            {
                throw new ApiException {
                    StatusCode = HttpStatusCode.Unauthorized,
                    newMessage = "The user is not allowed to perform this action."
                };
            }
        }
    }
}
