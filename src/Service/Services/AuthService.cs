using Domain.Dtos;
using Domain.Dtos.Token;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interfaces.Exceptions;
using Domain.Interfaces.User;
using Domain.Security;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly ApplicationDbContext _applicationContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;


        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParams,
            ApplicationDbContext applicationContext
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
            _applicationContext = applicationContext;
        }

        public async Task<AuthResult> Register(UserRegistrationDto user)
        {

            var existingEmail = await _userManager.FindByEmailAsync(user.Email);
            if (existingEmail != null)
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    newMessage = "Email already in use."
                };
            }

            var existingUsername = await _userManager.FindByNameAsync(user.UserName);
            if (existingUsername != null)
            {

                throw new ApiException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    newMessage = "Username already in use"
                };
            }


            var newUser = new ApplicationUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                DateOfBirth = user.BirthDate,
                CreatedAt = null
            };

            var isCreated = await _userManager.CreateAsync(newUser, user.Password);

            if (isCreated.Succeeded)
            {

                await _userManager.AddToRoleAsync(newUser, "Customer");

                var jwtToken = await GenerateJwtToken(newUser);

                return jwtToken;
            }
            else
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    newMessage = "User creation did not succeed."
                };
            }
        }

        public async Task<AuthResult> Login(UserLoginRequestDto user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    newMessage = "Invalid login request."
                };
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!isCorrect)
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    newMessage = "Invalid login request."
                };
            }

            var jwtToken = await GenerateJwtToken(existingUser);
            return jwtToken;
        }

        public async Task<AuthResult> RefreshToken(TokenRequest tokenRequest)
        {
            var result = await VerifyAndGenerateToken(tokenRequest);
            if (result == null)
            {
                throw new ApiException
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    newMessage = "Invalid token."
                };
            }

            return result;
        }



        private async Task<AuthResult> GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshTokenEntity()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()

            };

            await _applicationContext.RefreshTokens.AddAsync(refreshToken);
            await _applicationContext.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        /**
         * Get all valid claims for the corresponding user.
         */
        private async Task<List<Claim>> GetAllValidClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }

                }
            }

            return claims;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                //Validation 1 - Validation JWT token format - token validation params are equal to the one passed in startup
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

                //Validation 2 - Validate encryption algorithm - token has the same encryption algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {

                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        return null;
                    }
                }

                //Validation 3 - Validate expiry date - token was not expired
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has not yet expired"
                        }
                    };
                }

                //Validation 4 - Validate existance of the token - exist in database
                var storedToken = await _applicationContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if (storedToken == null)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token does not exist"
                        }
                    };

                }

                //Validation 5 - Validate if used - token already been used before
                if (storedToken.IsUsed)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has been used"
                        }
                    };
                }

                //Validation 6 - Validate if revoked - token has been revoked
                if (storedToken.IsRevoked)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has been revoked"
                        }
                    };
                }

                //Validation 7 - Validate the id - check the jwt id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new AuthResult()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token does not match"
                        }
                    };

                }

                //update current token
                storedToken.IsUsed = true;
                _applicationContext.RefreshTokens.Update(storedToken);
                await _applicationContext.SaveChangesAsync();

                //Generate a new token
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId.ToString());
                return await GenerateJwtToken(dbUser);
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var datetTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            datetTimeVal = datetTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
            return datetTimeVal;
        }
    }
}
