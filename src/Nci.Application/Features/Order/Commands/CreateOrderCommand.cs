using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Order.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Order.Commands
{
    public class CreateOrderCommand : OrderCreateDto, IRequest<OrderResponse>
    {
        public int UserId { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderEntity>(request);
            order.UserId = request.UserId;
            order.OrderDate = DateTime.Now;
            order.Status = 1;
            order.Amount = request.Amount;
            order.PaymentMethod = request.PaymentMethod;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);
            
            AddCartItemsToOrderItem(request.UserId, order.Id);
            RemoveItemsFromCart(request.UserId);
            _context.SaveChanges();

            return _mapper.Map<OrderResponse>(order);
        }
        private void AddCartItemsToOrderItem(int pUserId, int pOrderId)
        {
            var cartEntities = _context.ShoppingCart.Where(ci => ci.UserId == pUserId).ToList();
            
            OrderItemEntity[] orderItemAddRangeModel = new OrderItemEntity[cartEntities.Count];
            int counter = 0;
            foreach (var item in cartEntities)
            {
                orderItemAddRangeModel[counter] = new OrderItemEntity
                {
                 OrderId = pOrderId,
                 Price = item.Price,
                 ProductId = item.ProductId,
                 Quantity = item.Quantity,
                };
                counter++;
            }
            _context.OrderItem.AddRange(orderItemAddRangeModel);
           
        }

        private void RemoveItemsFromCart(int pUserId)
        {
            _context.ShoppingCart.RemoveRange(_context.ShoppingCart.Where(x => x.UserId == pUserId));

        }
    }

}
