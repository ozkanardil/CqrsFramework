using AutoMapper;
using CqrsFramework.Application.Features.Product.Commands;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Application.Features.Category.Commands;

namespace CqrsFramework.Application.Features.Product.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductResponse>().ReverseMap();
            CreateMap<CreateProductCommand, ProductEntity>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductEntity>().ReverseMap();
        }
    }
}
