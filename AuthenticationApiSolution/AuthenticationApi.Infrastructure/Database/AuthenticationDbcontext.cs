using AuthenticationApi.Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApi.Infrastructure.Database
{
    public class AuthenticationDbcontext : DbContext
    {
        public AuthenticationDbcontext(DbContextOptions<AuthenticationDbcontext> options) : base(options) { }
               public  DbSet<AppUser> User { get; set; } 
    }
}
