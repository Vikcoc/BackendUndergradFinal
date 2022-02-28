using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Services;
using Services.Exceptions;

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
        public async Task<ActionResult<Guid>> PostToAwsAsync(IFormFile file)
        {
            if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
            {
                throw new BadRequestException(ErrorStrings.InvalidFile);
            }
            
            var result = await _mediaService.AddPhotoAsync(file);

            return Ok(result);
        }
    }
}
