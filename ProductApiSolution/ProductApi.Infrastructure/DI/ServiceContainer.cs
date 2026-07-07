using eCommerce.SharedLibary.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Database;
using ProductApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.DI
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services , IConfiguration config) 
        {
            SharedServicesContainer.AddSharedService<ProductDbcontext>(services, config, config["MySeriLog:FileName"]!);
            services.AddScoped<IProduct, ProductRepository>();
            return services;

        }
        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app) 
        {
            SharedServicesContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
