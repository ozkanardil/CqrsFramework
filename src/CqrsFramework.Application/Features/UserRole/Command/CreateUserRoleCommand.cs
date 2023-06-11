using AutoMapper;
using MediatR;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Application.Features.UserRole.Constant;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.UserRole.Command
{
    public class CreateUserRoleCommand : IRequest<IRequestResult>
    {
        public string userEmailAdd { get; set; }
        public int userRoleIdAdd { get; set; }
    }

    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, IRequestResult>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CreateUserRoleCommandHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestResult> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(u => u.Email == request.userEmailAdd).FirstOrDefault();
            UserRoleEntity roleEntity = new UserRoleEntity();
            roleEntity.RoleId = request.userRoleIdAdd;
            roleEntity.UserId = user.Id;

            _context.UserRole.Add(roleEntity);
            int numAffectedRecords = await _context.SaveChangesAsync(cancellationToken);

            if (numAffectedRecords == 0)
                return new ErrorRequestResult(Messages.UserRoleAddError);

            return new SuccessRequestResult(Messages.UserRoleAddSuccess);
        }
    }
}
