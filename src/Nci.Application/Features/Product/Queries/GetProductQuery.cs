using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Application.Features.Category.Queries;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Product.Queries
{
    public class GetProductQuery : IRequest<IEnumerable<ProductResponse>>
    {

    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Products.AsQueryable();
            var response = await result.ToListAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(response).ToList();
             
        }
    }
}
