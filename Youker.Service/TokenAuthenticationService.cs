using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Youker.Application;

namespace Youker.Service
{
    public class TokenAuthenticationService
    {
        private readonly TokenConfig _tokenManagement;
        public TokenAuthenticationService(IOptions<TokenConfig> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value;
        }
        public bool IsAuthenticated(int userid, out string token)
        {
            token = string.Empty;
            //if (!_userService.IsValid(request))
            //    return false;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userid.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }

    }
   
}
