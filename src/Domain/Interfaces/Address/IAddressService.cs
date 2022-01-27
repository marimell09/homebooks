using Domain.Dtos.Address;
using Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Address
{
    public interface IAddressService
    {
        Task<AddressDto> Get(Guid id);
        Task<IEnumerable<AddressDto>> GetAll();
        Task<AddressCreateResponseDto> Post(AddressCreateDto user);
        Task<AddressUpdateResponseDto> Put(AddressUpdateDto user);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<AddressDto>> FindAddressesByLogin(Guid userId);
    }
}
