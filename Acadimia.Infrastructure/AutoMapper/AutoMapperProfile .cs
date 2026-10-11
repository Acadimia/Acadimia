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
            CreateMap<User, UserDto>().ReverseMap()
    .ForMember(d => d.Gender, o => o.Ignore())
    .ForMember(d => d.UserType, o => o.Ignore());

            CreateMap<User, MyProfileDto>().ReverseMap()
                .ForMember(d => d.Gender, o => o.Ignore());
        }
    }
}