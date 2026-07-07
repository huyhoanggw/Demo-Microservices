using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Database
{
    public class OrderDbcontextFactory : IDesignTimeDbContextFactory<OrderDbcontext>
    {
        public OrderDbcontext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"D:\MicroServicesProject\OrderApiSolution\OrderApi.Presentation\appsettings.json").Build();
            var options = new DbContextOptionsBuilder<OrderDbcontext>();
            options.UseSqlServer(config.GetConnectionString("eCommerceConnection"));
            return new OrderDbcontext(options.Options);
                
        }
    }
}
