using AutoMapper;
using CqrsFramework.Application.Features.Auth.Models;
using CqrsFramework.Application.Features.UserRole.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Infrastructure.Security.JwtToken;

namespace CqrsFramework.Application.Features.Auth.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserRoleVEntity, UserRoleResponse>().ReverseMap();
            
            CreateMap<AccessToken, TokenResult>().ReverseMap();

            CreateMap<UserRoleEntity, RoleEntity>().ReverseMap();

            CreateMap<UserRoleEntity, UserRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Role))
                .ReverseMap();
        }
    }
}
