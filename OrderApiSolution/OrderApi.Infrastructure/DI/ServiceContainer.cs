using eCommerce.SharedLibary.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Interface;
using OrderApi.Application.Service;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Database;
using OrderApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.DI
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config) 
        {
            SharedServicesContainer.AddSharedService<OrderDbcontext>(services, config, config["MySeriLog:FileName"]!);
            services.AddScoped<IOrder, OrderRepository>();
            return services;
        }
        public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
        {
            SharedServicesContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
