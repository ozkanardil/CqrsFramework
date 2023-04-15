using AutoMapper;
using CqrsFramework.Application.Features.Order.Commands;
using CqrsFramework.Application.Features.Order.Models;
using CqrsFramework.Application.Features.OrderItem.Models;
using CqrsFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.OrderItem.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemEntity, OrderItemResponse>().ReverseMap();
        }
    }
}
