using Domain.Entities;
using Domain.Interfaces.Exceptions;
using Domain.Interfaces.User;
using Domain.Security;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoleService : IRoleService
    {

        private readonly ApplicationDbContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            ApplicationDbContext applicationContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<RoleService> logger)
        {
            _logger = logger;
            _applicationContext = applicationContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IEnumerable<IdentityRole<Guid>> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }

        public async Task<IdentityRole<Guid>> CreateRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);
            if (roleExist)
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    newMessage = "Role already exist."
                };
            }
            IdentityRole<Guid> identityRole = new IdentityRole<Guid>(name);
            var roleResult = await _roleManager.CreateAsync(identityRole);

            if (!roleResult.Succeeded)
            {
                _logger.LogInformation($"The role {name} has not been added.");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    newMessage = $"The role { name } has not been added."
                };
            }

            _logger.LogInformation($"The role {name} has been added successfully");
            return identityRole;
        }

        public async Task<IdentityResult> AddUserToRole(string email, string roleName)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    newMessage = "User does not exist"
                };
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role with {roleName} does not exist");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    newMessage = "Role does not exist."
                };
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"The user was not able to be added to the role");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    newMessage = "The user was not able to be added to the role."
                };
            }

            return result;
        }

        public async Task<IEnumerable> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    newMessage = "User does not exist."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task<IdentityResult> RemoveUserFromRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    newMessage = "User does not exist."
                };
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {

                throw new ApiException
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    newMessage = $"Unable to remove user { email } from role { roleName }."
                };
            }

            return result;
        }
    }
}
