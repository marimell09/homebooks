using Domain.Entities;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")] //api/setup
    public class SetupController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetupController> _logger;

        public SetupController(
            ApplicationDbContext applicationContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<SetupController> logger)
        {
            _logger = logger;
            _applicationContext = applicationContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"The role {name} has been added successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been added successfully"
                    });
                }
                else
                {
                    _logger.LogInformation($"The role {name} has not been added");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been added"
                    });
                }
            }

            return BadRequest(new {error = "Role already exist" });
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role with {roleName} does not exist");
                return BadRequest(new { error = "Role does not exist" });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded){
                return Ok(new { result = "Success, user has been added to the role" });
            }
            else
            {
                _logger.LogInformation($"The user was not able to be added to the role");
                return BadRequest(new { error = "The user was not able to be added to the role" });
            }
        }

        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost]
        [Route("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"User has been removed from role {roleName}"
                });
            }

            return BadRequest(new { error = $"Unable to remove user {email} from role {roleName}" });
        }
    }
}
