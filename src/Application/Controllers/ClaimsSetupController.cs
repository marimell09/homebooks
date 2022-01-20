using Domain.Security;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsSetupController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MyContext _myContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthManagementController> _logger;

        public ClaimsSetupController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AuthManagementController> logger,
            MyContext myContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _myContext = myContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            return Ok(userClaims);
        }

        [HttpPost]
        [Route("AddClaimsToUser")]
        public async Task<IActionResult> AddClaimsToUser(string email, string claimName, string claimValue)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"The user with {email} does not exist");
                return BadRequest(new { error = "User does not exist" });
            }

            var userClaim = new Claim(claimName, claimValue);
            var result = await _userManager.AddClaimAsync(user, userClaim);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"User {user.Email} has a claim {claimName} added to them"
                });

            }
            return BadRequest(new { error = $"Unable to add claim {claimName} to the user {user.Email}" });
        }

    }
}
