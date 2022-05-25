using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class WaterSourcePlaceService
    {
        private readonly MyEfDbContext _dbContext;

        public WaterSourcePlaceService(MyEfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WaterSourceContribution> AddPlace(WaterSourcePlace place, List<Guid> pictures, Guid userId)
        {
            var dbPictures = await _dbContext.WaterSourcePictures.Where(x => pictures.Contains(x.Id)).ToListAsync();
            place.Pictures.AddRange(dbPictures);
            try
            {
                await _dbContext.WaterSourcePlaces.AddAsync(place);
                var contribution = new WaterSourceContribution
                {
                    ContributionType = ContributionType.Creation,
                    Details = "Creation",
                    WaterSourcePlace = place,
                    WaterUserId = userId
                };
                await _dbContext.WaterSourceContributions.AddAsync(contribution);
                await _dbContext.SaveChangesAsync();
                return contribution;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
