using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.DI
{
    public static  class JWTAuthenticationScheme
    {
        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:KEY").Value!);
                var issue = config.GetSection("Authentication:Issuer").Value;
                var audience = config.GetSection("Authentication:Audience").Value;
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters 
                // xác thực token gồm issuer , audience , lifetime , signingkey
                {
                    // cho phép xác thực issuer , audience , lifetime , signingkey 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    // truyền tham số xác thực cho issuer , audience , signingkey 
                    ValidIssuer = issue, 
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }; 
            });
            return services;
        }
    }
}
