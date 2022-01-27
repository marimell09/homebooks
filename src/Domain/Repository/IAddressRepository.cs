using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IAddressRepository : IRepository<AddressEntity>
    {
        Task<IEnumerable<AddressEntity>> FindAddressesByLogin(Guid userId);
    }
}
