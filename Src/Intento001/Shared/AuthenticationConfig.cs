using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Intento001.Shared
{
    public static class AuthenticationConfig
    {
        public const string key = "CB1E08171A72D65C8AC9CD758531FD55B104F81918D0A20C2EFFC3EE533003DC";
        public static string CreateJsonWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("userName","Admin")
            };
            var token = new JwtSecurityToken(
                "Http://localhost:52875",
                "Http://localhost:52875",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
