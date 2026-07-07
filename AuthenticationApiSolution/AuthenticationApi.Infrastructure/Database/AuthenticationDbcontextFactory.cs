using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Infrastructure.Database
{
    public class AuthenticationDbcontextFactory : IDesignTimeDbContextFactory<AuthenticationDbcontext>

    {
        public AuthenticationDbcontext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(@"D:\MicroServicesProject\AuthenticationApiSolution\AuthenticationApi.Presentation\appsettings.json").Build();
            var options = new DbContextOptionsBuilder<AuthenticationDbcontext>();
            options.UseSqlServer(config.GetConnectionString("eCommerceConnection"));
            return new AuthenticationDbcontext(options.Options);
        }
    }
}
