using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MyEfDbContextFactory : IDbContextFactory<MyEfDbContext>
    {
        public MyEfDbContext CreateDbContext()
        {
            return new MyEfDbContext();
        }
    }
}
