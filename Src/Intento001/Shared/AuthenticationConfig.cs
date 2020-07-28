﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
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
                "xyz5",
                "xyz5",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        internal static TokenValidationParameters validationParameters;

        public static void ConfigureJwtAuthentication(this IServiceCollection services)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("userName","Admin")
            };
            validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = "xyz5",
                ValidateLifetime = true,
                ValidAudience = "xyz5",
                ValidateAudience = true,
                RequireSignedTokens = true,
                IssuerSigningKey = credentials.Key,
                ClockSkew = TimeSpan.FromMinutes(30)
            };
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = validationParameters;
#if PROD || UAT
                options.IncludeErrorDetails=false;
#else
                options.RequireHttpsMetadata = false;
#endif
            });

        }
    }
}
