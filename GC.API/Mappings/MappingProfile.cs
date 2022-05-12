using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GF.DAL.Entities;
using GC.DTO.Responses;

namespace GC.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserResponseDTO, User>();

            CreateMap<AccessToken, TokenResponseDTO>();
            CreateMap<TokenResponseDTO, AccessToken>();

            CreateMap<Blog, BlogResponseDTO>().ForMember(dto => dto.Authors, opt => opt.MapFrom(x => x.Authors.Select(y => y.User).ToList()));
        }
    }
}