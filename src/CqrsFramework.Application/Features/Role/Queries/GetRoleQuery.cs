using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Infrastructure.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Application.Features.Role.Models;
using CqrsFramework.Application.Features.Role.Constants;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Role.Queries
{
    public class GetRoleQuery : IRequest<IRequestDataResult<IEnumerable<RoleResponse>>>
    {
    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IRequestDataResult<IEnumerable<RoleResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<RoleResponse>>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Role.ToListAsync();
            var response = _mapper.Map<IEnumerable<RoleResponse>>(result);

            if (!response.Any())
                return new ErrorRequestDataResult<IEnumerable<RoleResponse>>(response, Messages.RolesListError);

            return new SuccessRequestDataResult<IEnumerable<RoleResponse>>(response, Messages.RolesListed); ;
        }
    }
}
