using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.User.Constants;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.User.Queries
{
    public class GetUserQuery : IRequest<IRequestDataResult<IEnumerable<UserResponse>>>
    {

    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IRequestDataResult<IEnumerable<UserResponse>>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IMapper mapper, DatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<IRequestDataResult<IEnumerable<UserResponse>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.User.Where(u => u.Status == 1).ToListAsync();
            var response = _mapper.Map<IEnumerable<UserResponse>>(result);

            if(!response.Any())
                return new ErrorRequestDataResult<IEnumerable<UserResponse>>(response, Messages.UserNoRecord);

            return new SuccessRequestDataResult<IEnumerable<UserResponse>>(response, Messages.UserListed); ;
        }
    }
}
