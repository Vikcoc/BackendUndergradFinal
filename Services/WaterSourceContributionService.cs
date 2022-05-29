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
    public class WaterSourceContributionService
    {
        private readonly MyEfDbContext _dbContext;

        public WaterSourceContributionService(MyEfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WaterSourceContribution>> GetLatestUserContributionsAsync(Guid userId, int skip, int take)
        {
            return await _dbContext.WaterSourceContributions
                .Where(x => x.WaterUserId == userId)
                .Include(x => x.WaterSourcePlace)
                .ThenInclude(x => x.Pictures.OrderBy(y => y.CreatedAt).Take(1))
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
