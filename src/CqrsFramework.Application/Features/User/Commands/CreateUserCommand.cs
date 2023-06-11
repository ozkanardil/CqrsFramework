using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.Product.Commands;
using CqrsFramework.Application.Features.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Application.Features.User.Constants;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Application.Features.User.Rules;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.User.Commands
{
    public class CreateUserCommand : UserModel, IRequest<IRequestResult>
    {
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IMapper mapper,
                                        DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            GuardUserCreate.Against(request)
                .Null()
                .KeepGoing();


            var user = _mapper.Map<UserEntity>(request);
            user.Status = 1;
            _context.User.Add(user);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.UserAddError);

            return new SuccessRequestResult(Messages.UserAddSuccess);
        }
    }
}
