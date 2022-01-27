using AutoMapper;
using Domain.Dtos.Address;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Address;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AddressService : IAddressService
    {
        private IRepository<AddressEntity> _repository;
        private readonly IMapper _mapper;
        public AddressService(IRepository<AddressEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AddressDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<AddressDto>(entity);
        }

        public async Task<IEnumerable<AddressDto>> GetAll()
        {
            var entities = await _repository.SelectAsync();
            return _mapper.Map<IEnumerable<AddressDto>>(entities);
        }

        public async Task<AddressCreateResponseDto> Post(AddressCreateDto user)
        {
            var model = _mapper.Map<AddressModel>(user);
            var entity = _mapper.Map<AddressEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<AddressCreateResponseDto>(result);
        }

        public async Task<AddressUpdateResponseDto> Put(AddressUpdateDto user)
        {
            var model = _mapper.Map<AddressModel>(user);
            var entity = _mapper.Map<AddressEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<AddressUpdateResponseDto>(result);
        }
    }
}
