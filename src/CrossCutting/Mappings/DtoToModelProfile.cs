using AutoMapper;
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
            CreateMap<UserModel, UserDtoCreate>()
              .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
              .ReverseMap();
            #endregion
        }
    }
}
