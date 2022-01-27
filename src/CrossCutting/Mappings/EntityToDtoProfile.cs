using AutoMapper;
using Domain.Dtos.Address;
using Domain.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            #region User
            CreateMap<UserDto, ApplicationUser>()
              .ReverseMap();

            CreateMap<UserRegistrationResponseDto, ApplicationUser>()
              .ReverseMap();

            CreateMap<UserDtoUpdateResult, ApplicationUser>()
              .ReverseMap();
            #endregion

            #region Address
            CreateMap<AddressDto, AddressEntity>()
                .ReverseMap();

            CreateMap<AddressCreateResponseDto, AddressEntity>()
                .ReverseMap();

            CreateMap<AddressUpdateResponseDto, AddressEntity>()
                .ReverseMap();
            #endregion
        }
    }
}
