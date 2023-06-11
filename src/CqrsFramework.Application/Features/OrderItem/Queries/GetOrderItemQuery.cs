using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.Order.Models;
using CqrsFramework.Infrastructure.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Application.Features.OrderItem.Models;
using CqrsFramework.Application.Features.OrderItem.Constants;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.OrderItem.Queries
{
    public class GetOrderItemQuery : IRequest<IRequestDataResult<IEnumerable<OrderItemResponse>>>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, IRequestDataResult<IEnumerable<OrderItemResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetOrderItemQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<OrderItemResponse>>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.OrderItem.Where(oi => oi.OrderId == request.OrderId).Include(t => t.Product).ToListAsync();
            var response = _mapper.Map<IEnumerable<OrderItemResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<OrderItemResponse>>(response, Messages.OrderItemNoRecord);

            return new SuccessRequestDataResult<IEnumerable<OrderItemResponse>>(response, Messages.OrderItemsListed); ;
        }
    }
}
