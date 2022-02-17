using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.User
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole<Guid>> GetAllRoles();
        Task<IdentityRole<Guid>> CreateRole(string name);
        Task<IdentityResult> AddUserToRole(string email, string roleName);
        Task<IEnumerable> GetUserRoles(string email);
        Task<IdentityResult> RemoveUserFromRole(string email, string roleName);
    }
}
