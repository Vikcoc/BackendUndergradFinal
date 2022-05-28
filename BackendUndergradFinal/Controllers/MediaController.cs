using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Exceptions;
using System;
using System.Threading.Tasks;

namespace BackendUndergradFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerWithError
    {
        private readonly MediaService _mediaService;

        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet("{imageId:guid}")]
        public async Task<FileStreamResult> GetImageAsync(Guid imageId)
        {
            return File(await _mediaService.GetImageAsync(imageId), "image/jpeg");
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.RequestSizeLimit(51486000)]
        public async Task<ActionResult<Guid>> PostImageAsync(IFormFile picture)
        {
            if (picture.ContentType != "image/jpeg" && picture.ContentType != "image/png")
            {
                throw new BadRequestException(ErrorStrings.InvalidFile);
            }

            var result = await _mediaService.AddPhotoAsync(picture);

            return Ok(result);
        }
    }
}
