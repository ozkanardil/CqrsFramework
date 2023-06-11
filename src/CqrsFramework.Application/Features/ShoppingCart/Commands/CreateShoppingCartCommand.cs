using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Application.Features.ShoppingCart.Constants;
using CqrsFramework.Application.Features.ShoppingCart.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.ShoppingCart.Commands
{
    public class CreateShoppingCartCommand : ShoppingCartCreateDto, IRequest<IRequestResult>
    {
        public int UserId { get; set; }
    }

    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateShoppingCartCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestResult> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = _mapper.Map<ShoppingCartEntity>(request);
            shoppingCart.UserId = request.UserId;
            shoppingCart.AddDate = DateTime.Now;

            _context.ShoppingCart.Add(shoppingCart);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.ShoppingCartAddError);

            return new SuccessRequestResult(Messages.ShoppingCartAddSuccess);
        }
    }
}
