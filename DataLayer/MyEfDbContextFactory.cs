using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataLayer
{
    public class MyEfDbContextFactory : IDesignTimeDbContextFactory<MyEfDbContext>
    {
        public MyEfDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"..\BackendUndergradFinal"))
                .AddJsonFile("appsettings.Local.json", true)
                .Build();
            DbContextOptionsBuilder x = new DbContextOptionsBuilder();
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new MyEfDbContext(x.Options);
        }
    }
}
