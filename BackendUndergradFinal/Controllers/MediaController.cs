using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

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
        public async Task<Stream> GetImageAsync(Guid imageId)
        {
            return await _mediaService.GetImageAsync(imageId);
        }
    }
}
