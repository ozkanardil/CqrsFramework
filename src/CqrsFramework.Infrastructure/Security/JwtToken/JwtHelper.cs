using Microsoft.IdentityModel.Tokens;
using CqrsFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure.Security.JwtToken
{
    public class JwtHelper : ITokenHelper
    {
        private DateTime _tokenExpirationDate;
        public JwtHelper()
        {
            _tokenExpirationDate = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(UserEntity user, List<UserRoleDto> userClaims)
        {
            var securityKey = CreateSecurityKey();
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityToken(user, signingCredentials, userClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _tokenExpirationDate
            };
        }

        public SecurityKey CreateSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.SecurityKey));
        }

        private JwtSecurityToken CreateJwtSecurityToken(UserEntity user,
           SigningCredentials signingCredentials, List<UserRoleDto> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                expires: _tokenExpirationDate,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(UserEntity user, List<UserRoleDto> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.Name}");
            claims.AddRoles(operationClaims.Select(c => c.Name.Trim()).ToArray());

            return claims;
        }

    }
}
