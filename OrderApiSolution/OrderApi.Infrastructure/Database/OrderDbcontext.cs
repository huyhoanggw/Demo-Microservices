using Microsoft.EntityFrameworkCore;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Database
{
    public class OrderDbcontext : DbContext
    {
        public OrderDbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Order> Order { get; set;  }
    
        }
}
