using AutoMapper;
using CqrsFramework.Application.Features.Role.Models;
using CqrsFramework.Domain.Entities;

namespace CqrsFramework.Application.Features.Role.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEntity, RoleResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
        }
    }
}
