using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class MyEfDbContextFactory : IDesignTimeDbContextFactory<MyEfDbContext>
    {
        public MyEfDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"..\BackendUndergradFinal"))
                .AddJsonFile($"appsettings.Local.json", true)
                .Build();
            DbContextOptionsBuilder x = new DbContextOptionsBuilder();
            x.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            return new MyEfDbContext(x.Options);
        }
    }
}
