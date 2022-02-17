using Domain.Dtos;
using Domain.Dtos.Token;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.User
{
    public interface IAuthService
    {
        Task<AuthResult> Register(UserRegistrationDto user);
        Task<AuthResult> Login(UserLoginRequestDto user);
        Task<AuthResult> RefreshToken(TokenRequest tokenRequest);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
    }
}
