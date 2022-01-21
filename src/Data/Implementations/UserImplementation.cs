using Domain.Entities;
using Domain.Repository;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class UserImplementation //: BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<ApplicationUser> _dataset;

        public UserImplementation(ApplicationDbContext context)// : base(context)
        {
            _dataset = context.Set<ApplicationUser>();
        }

        public async Task<ApplicationUser> FindByLogin(string email)
        {
            return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
