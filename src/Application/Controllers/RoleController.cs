using Domain.Entities;
using Domain.Interfaces.Exceptions;
using Domain.Interfaces.User;
using Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")] //api/role
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    public class RoleController : ControllerBase
    {
        private IRoleService _service;
        private ILogger<RoleController> _logger;

        public RoleController(IRoleService service, ILogger<RoleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _service.GetAllRoles();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            try
            {
                var role = await _service.CreateRole(name);
                return NoContent();
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
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            try
            {
                var user = await _service.AddUserToRole(email, roleName);
                return NoContent();
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
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            try
            {
                var roles = await _service.GetUserRoles(email);
                return Ok(roles);
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
        [Route("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            try
            {
                var user = await _service.RemoveUserFromRole(email, roleName);
                return NoContent();
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
    }
}
