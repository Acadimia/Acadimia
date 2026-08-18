using AutoMapper;
using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<,>().ReverseMap()
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, MyProfileDto>().ReverseMap();
        }
    }
}