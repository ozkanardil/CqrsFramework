using AutoMapper;
using CqrsFramework.Application.Features.Product.Commands;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Application.Features.User.Commands;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Application.Features.UserRole.Command;
using CqrsFramework.Application.Features.UserRole.Models;
using CqrsFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.UserRole.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRoleVEntity, UserRoleResponse>().ReverseMap();
            CreateMap<CreateUserRoleCommand, UserRoleEntity>().ReverseMap();
        }
    }
}
