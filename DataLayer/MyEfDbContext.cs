using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MyEfDbContext : IdentityDbContext<MyAppUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<TestClass> TestClasses { get; set; }

        public MyEfDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
