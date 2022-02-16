using Domain.Entities;
using Domain.Interfaces.Exceptions;
using Domain.Interfaces.User;
using Domain.Security;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClaimsService : IClaimsService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<ClaimsService> _logger;

        public ClaimsService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<ClaimsService> logger,
            ApplicationDbContext applicationContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Claim>> GetAllClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);


            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User does not exist."
                };
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            return userClaims;
        }

        public async Task<Claim> AddClaimsToUser(string email, string claimName, string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User does not exist."
                };
            }

            var userClaim = new Claim(claimName, claimValue);
            var result = await _userManager.AddClaimAsync(user, userClaim);

            if (!result.Succeeded)
            {
                _logger.LogInformation($"The claim addition to the user with {email} couldn't be performed.");
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = $"Unable to add claim {claimName} to the user {user.Email}."
                };
            }

            return userClaim;
        }
    }
}
