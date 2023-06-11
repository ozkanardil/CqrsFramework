using CqrsFramework.Application.Features.UserRole.Models;
using CqrsFramework.Infrastructure.Security.JwtToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Auth.Models
{
    public class LoginResponse
    {
        public TokenResult Token { get; set; }
        public List<UserRoleResponse> Roles { get; set; }
    }

    public class TokenResult : AccessToken
    {
        public string refreshToken { get; set; }
    }

}
