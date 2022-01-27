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
    public class AddressImplementation: BaseRepository<AddressEntity>, IAddressRepository
    {
        private DbSet<AddressEntity> _dataset;

        public AddressImplementation(ApplicationDbContext context) : base(context)
        {
            _dataset = context.Set<AddressEntity>();
        }
    }
}
