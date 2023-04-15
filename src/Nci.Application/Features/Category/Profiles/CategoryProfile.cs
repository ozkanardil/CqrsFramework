using AutoMapper;
using CqrsFramework.Application.Features.Category.Commands;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Domain.Entities;

namespace CqrsFramework.Application.Features.Category.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryResponse>().ReverseMap();
            CreateMap<CreateCategoryCommand, CategoryEntity>().ReverseMap();
            CreateMap<UpdateCategoryCommand, CategoryEntity>().ReverseMap();
        }
    }
}
