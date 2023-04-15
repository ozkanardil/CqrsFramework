using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Application.Features.Auth.Constants;
using CqrsFramework.Application.Features.Auth.Models;
using CqrsFramework.Application.Features.UserRole.Models;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Infrastructure.Results;
using CqrsFramework.Infrastructure.Security.JwtToken;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Application.Features.Auth.Queries
{
    public class GetLoginQuery : IRequest<IRequestDataResult<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GetLoginQueryHandler : IRequestHandler<GetLoginQuery, IRequestDataResult<LoginResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private ITokenHelper _tokenHelper;

        public GetLoginQueryHandler(IMapper mapper, DatabaseContext context, ITokenHelper tokenHelper)
        {
            _mapper = mapper;
            _context = context;
            _tokenHelper = tokenHelper;
        }
        public async Task<IRequestDataResult<LoginResponse>> Handle(GetLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
                return new ErrorRequestDataResult<LoginResponse>(null, Messages.UserNotFound);

            var userClaims = await _context.UserRole.Where(ur => ur.UserId == user.Id).Include(r=>r.Role).ToListAsync();
            var claims = _mapper.Map<List<UserRoleDto>>(userClaims);

            var token = _tokenHelper.CreateToken(user, claims);

            var userVClaims = await _context.UserRoleV.Where(ur => ur.UserId == user.Id).ToListAsync();
            foreach (var item in userVClaims)
            {
                item.Role = item.Role.Trim();
            }

            TokenResult tokenResult = new TokenResult();
            tokenResult.Token = token.Token;
            tokenResult.Expiration = token.Expiration;
            tokenResult.refreshToken = "";

            LoginResponse result = new LoginResponse();
            result.Token = tokenResult;
            result.Roles = _mapper.Map<List<UserRoleResponse>>(userVClaims);

            if (token == null)
                return new ErrorRequestDataResult<LoginResponse>(result, Messages.UserLoginErr);

            return new SuccessRequestDataResult<LoginResponse>(result, Messages.UserLoginOk); ;
        }
    }
}
