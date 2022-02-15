using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Services.Exceptions;

namespace Services
{
    public class MediaService
    {
        private readonly MyEfDbContext _dbContext;

        public MediaService(MyEfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileStream> GetImageAsync(Guid imageId)
        {
            var img = await _dbContext.WaterSourcePictures.FindAsync(imageId);
            if (img == default || !File.Exists(img.Uri))
                throw new BadRequestException(ErrorStrings.NoImageEntry);
            return new FileStream(img.Uri, FileMode.Open, FileAccess.Read);
        }
    }
}
