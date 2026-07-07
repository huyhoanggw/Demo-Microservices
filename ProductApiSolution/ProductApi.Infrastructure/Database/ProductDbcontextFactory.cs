using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Database
{
    public class ProductDbcontextFactory : IDesignTimeDbContextFactory<ProductDbcontext>
    {   
        public ProductDbcontext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile(@"D:\MicroServicesProject\ProductApiSolution\ProductApi.Presentation\appsettings.json")
          .Build();
            var options = new DbContextOptionsBuilder<ProductDbcontext>();
            options.UseSqlServer(config.GetConnectionString("eCommerceConnection"));
            return new ProductDbcontext(options.Options);

        }
    }
}
