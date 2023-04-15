using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Order.Constants;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Order.Commands
{
    public class DeleteOrderCommand : IRequest<IRequestResult>
    {
        public DeleteOrderCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IRequestResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var Order = _context.Orders.SingleOrDefault(o => o.Id == request.Id);
            _context.Orders.Remove(Order);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.OrderDeleteError);

            return new SuccessRequestResult(Messages.OrderDeleted);
        }
    }
}
