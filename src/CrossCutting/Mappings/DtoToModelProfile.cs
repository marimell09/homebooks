using AutoMapper;
using Domain.Dtos.Address;
using Domain.Dtos.User;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region User
            CreateMap<UserModel, UserDto>()
              .ReverseMap();
            CreateMap<UserModel, UserRegistrationDto>()
              .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
              .ReverseMap();
            #endregion

            #region Address
            CreateMap<AddressModel, AddressDto>()
                .ReverseMap();
            CreateMap<AddressModel, AddressCreateDto>()
                .ReverseMap();
            CreateMap<AddressModel, AddressUpdateDto>()
                .ReverseMap();
            #endregion
        }
    }
}
