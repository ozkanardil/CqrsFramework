using AutoMapper;
using CqrsFramework.Application.Features.Order.Commands;
using CqrsFramework.Application.Features.Order.Models;
using CqrsFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Order.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateDto, CreateOrderCommand>().ReverseMap();
            CreateMap<OrderEntity, OrderResponse>().ReverseMap();
            CreateMap<CreateOrderCommand, OrderEntity>().ReverseMap();
        }
    }
}
