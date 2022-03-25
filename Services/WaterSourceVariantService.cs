using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _dbContext.WaterSourceVariants.Include(x => x.Pictures.OrderBy(y => y.Id).Take(1)).AsNoTracking().ToListAsync();
        }
    }
}
