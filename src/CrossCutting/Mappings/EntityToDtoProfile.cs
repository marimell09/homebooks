using AutoMapper;
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
            CreateMap<UserDto, ApplicationUser>()
              .ReverseMap();

            CreateMap<UserRegistrationResponseDto, ApplicationUser>()
              .ReverseMap();

            CreateMap<UserDtoUpdateResult, ApplicationUser>()
              .ReverseMap();
        }
    }
}
