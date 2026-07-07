using AuthenticationApi.Application.Interfaces;
using AuthenticationApi.Infrastructure.Database;
using AuthenticationApi.Infrastructure.Repository;
using eCommerce.SharedLibary.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Infrastructure.DI
{
    public static class ServiceContainer 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service , IConfiguration config)
        {
            SharedServicesContainer.AddSharedService<AuthenticationDbcontext>(service, config, config["MySeriLog:FileName"]!);
            service.AddScoped<IUser , UserRepository>();
            return service;
        }
        public static IApplicationBuilder AddInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServicesContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
