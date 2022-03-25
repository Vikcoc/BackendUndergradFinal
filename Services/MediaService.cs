using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Services.Exceptions;
using System.IO;
using System.Threading.Tasks;
using Guid = System.Guid;

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

        public async Task<Guid> AddPhotoAsync(IFormFile file)
        {
            var path = Path.Combine("Pictures", "User");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, Guid.NewGuid() + Path.GetExtension(file.FileName));

            await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Close();
            var pic = new WaterSourcePicture
            {
                Uri = path
            };
            await _dbContext.WaterSourcePictures.AddAsync(pic);
            await _dbContext.SaveChangesAsync();
            return pic.Id;
        }
    }
}
