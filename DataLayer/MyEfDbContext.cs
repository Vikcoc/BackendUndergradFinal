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

        public MyEfDbContext()
        {
        }

        public MyEfDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlServer(
            //    "Server=DESKTOP-U28TOVR;Initial Catalog=databasename;User id=sa;Password=Abcd_1234;");
        }
    }
}
