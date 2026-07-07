using eCommerce.SharedLibary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibary.DI
{
    public static class SharedServicesContainer
    {
        public static IServiceCollection AddSharedService<Tcontext>(this IServiceCollection service , IConfiguration config , string fileName) where Tcontext : DbContext 
        {
            // add generic dbcontext 
            service.AddDbContext<Tcontext>(options => 
            {
                options.UseSqlServer(config.GetConnectionString("eCommerceConnection"), sqlOption => sqlOption.EnableRetryOnFailure(10));

            });

            // add serilog 
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .WriteTo.File(path: $"{fileName}",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zz [{level:u3}] {Message:lj} {NewLine} {Exception}}",
                rollingInterval: RollingInterval.Day).CreateLogger();
            // add jwt authentication
             JWTAuthenticationScheme.AddJWTAuthenticationScheme(service , config);
            return service;
        }
        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app) 
        {
            app.UseMiddleware<GlobalException>();
            app.UseMiddleware<ListenToOnlyApiGateway>();
            return app;
        }
    }
}
