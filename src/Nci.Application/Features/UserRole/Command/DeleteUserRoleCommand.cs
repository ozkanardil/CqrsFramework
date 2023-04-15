using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Application.Features.UserRole.Constant;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.UserRole.Command
{
    public class DeleteUserRoleCommand : IRequest<IRequestResult>
    {
        public int Id { get; set; }
    }

    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public DeleteUserRoleCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IRequestResult> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = _context.UserRole.SingleOrDefault(ur => ur.Id == request.Id);
            _context.UserRole.Remove(userRole);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.UserRoleDeleteError);

            return new SuccessRequestResult(Messages.UserRoleDeleted);
        }
    }
}
