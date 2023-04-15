using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.ShoppingCart.Constants;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.ShoppingCart.Commands
{
    public class DeleteShoppingCartCommand : IRequest<IRequestResult>
    {
        public DeleteShoppingCartCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DeleteShoppingCartCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IRequestResult> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = _context.ShoppingCart.SingleOrDefault(u => u.Id == request.Id);

            if (shoppingCart == null)
                return new ErrorRequestResult(Messages.ShoppingCartNotFound);

            _context.ShoppingCart.Remove(shoppingCart);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.ShoppingCartDeleteError);

            return new SuccessRequestResult(Messages.ShoppingCartDeleted);

        }
    }
}
