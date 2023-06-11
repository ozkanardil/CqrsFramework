using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Application.Features.UserRole.Models;
using CqrsFramework.Application.Features.UserRole.Constant;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.UserRole.Queries
{
    public class GetUserRoleQuery : IRequest<IRequestDataResult<IEnumerable<UserRoleResponse>>>
    {
        public string userEmail { get; set; }
    }

    public class GetUserRoleQueryHandler : IRequestHandler<GetUserRoleQuery, IRequestDataResult<IEnumerable<UserRoleResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetUserRoleQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<UserRoleResponse>>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            int userId = _context.User.SingleOrDefault(u => u.Email == request.userEmail).Id;
            var result = await _context.UserRoleV.Where(ur => ur.UserId == userId).ToListAsync();
            var response = _mapper.Map<IEnumerable<UserRoleResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<UserRoleResponse>>(response, Messages.UserRoleNoRecord);

            return new SuccessRequestDataResult<IEnumerable<UserRoleResponse>>(response, Messages.UserRolesListed); ;
        }
    }
}
