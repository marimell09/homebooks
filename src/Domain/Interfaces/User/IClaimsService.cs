using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.User
{
    public interface IClaimsService
    {
        Task<IEnumerable<Claim>> GetAllClaims(string email);

        Task<Claim> AddClaimsToUser(string email, string claimName, string claimValue);
    }
}
