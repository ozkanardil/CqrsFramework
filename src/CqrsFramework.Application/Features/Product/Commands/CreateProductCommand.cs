using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Category.Commands;
using CqrsFramework.Application.Features.Category.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Product.Commands
{
    public class CreateProductCommand : ProductModel, IRequest<ProductResponse>
    {
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(request);
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
