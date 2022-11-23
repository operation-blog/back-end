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
            CreateMap<User, UserResponseDTO>()
                .ForMember(user => user.RegisterDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd/MM/yyyy")));

            CreateMap<AccessToken, TokenResponseDTO>();

            CreateMap<Blog, BlogResponseDTO>()
                .ForMember(dto => dto.Authors, opt => opt.MapFrom(x => x.Authors.Select(y => y.User).ToList()))
                .ForMember(date => date.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToString("dd/MM/yyyy")));
        }
    }
}