using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.Database
{
    public class ProductDbcontext : DbContext
    {
        public ProductDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
