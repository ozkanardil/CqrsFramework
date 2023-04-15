using AutoMapper;
using CqrsFramework.Application.Features.ShoppingCart.Commands;
using CqrsFramework.Application.Features.ShoppingCart.Models;
using CqrsFramework.Domain.Entities;


namespace CqrsFramework.Application.Features.ShoppingCart.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartEntity, ShoppingCartResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();

            CreateMap<CreateShoppingCartCommand, ShoppingCartEntity>().ReverseMap();

            CreateMap<DeleteShoppingCartCommand, ShoppingCartEntity>().ReverseMap();

            CreateMap<CreateShoppingCartCommand, ShoppingCartCreateDto>().ReverseMap();
        }
    }
}
