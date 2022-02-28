using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class WaterSourceVariantService
    {
        private readonly MyEfDbContext _dbContext;

        public WaterSourceVariantService(MyEfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WaterSourceVariant>> GetSourceVariantsWithImageAsync()
        {
            return await _dbContext.WaterSourceVariants.Include(x => x.Pictures.Take(1)).AsNoTracking().ToListAsync();
        }
    }
}
