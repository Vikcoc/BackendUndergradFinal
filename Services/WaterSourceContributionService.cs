using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;

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

        public async Task<List<WaterSourceContribution>> GetLatestPlaceContributionsAsync(Guid placeId, int skip, int take)
        {
            return await _dbContext.WaterSourceContributions
                .Where(x => x.WaterSourcePlaceId == placeId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<WaterSourceContribution> AddContributionAsync(WaterSourceContribution contribution)
        {
            var latest = await _dbContext.WaterSourceContributions.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync(x => x.WaterSourcePlaceId == contribution.WaterSourcePlaceId && x.ContributionType != ContributionType.Update);
            if (contribution.ContributionType is ContributionType.CreateIncident &&
                latest.ContributionType is ContributionType.Creation or ContributionType.RemoveIncident
                || contribution.ContributionType is ContributionType.ConfirmIncident &&
                latest.ContributionType is ContributionType.CreateIncident
                || contribution.ContributionType is ContributionType.InfirmIncident &&
                latest.ContributionType is ContributionType.CreateIncident or ContributionType.ConfirmIncident
                || contribution.ContributionType is ContributionType.RemoveIncident &&
                latest.ContributionType is ContributionType.InfirmIncident)
            {
                await _dbContext.WaterSourceContributions.AddAsync(contribution);
                await _dbContext.SaveChangesAsync();
            }
            else
                throw new BadRequestException(ErrorStrings.BadSequence);
            return latest;
        }
    }
}
