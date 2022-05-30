using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Communication.SourceContributionDto;
using Communication.SourcePlaceDto;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Exceptions;

namespace BackendUndergradFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WaterSourcePlaceController : ControllerBase
    {
        private readonly WaterSourcePlaceService _waterSourcePlaceService;
        private readonly IMapper _mapper;

        public WaterSourcePlaceController(WaterSourcePlaceService waterSourcePlaceService, IMapper mapper)
        {
            _waterSourcePlaceService = waterSourcePlaceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<WaterSourceContributionDto>> AddWaterSourcePlace(WaterSourcePlaceCreateDto createDto)
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _waterSourcePlaceService.AddPlace(_mapper.Map<WaterSourcePlace>(createDto), createDto.Pictures, id);
            return Ok(_mapper.Map<WaterSourceContributionDto>(res));
        }

        [HttpGet("get_in_rectangle/{left}/{bottom}/{right}/{top}")]
        public async Task<ActionResult> GetInRectangleAsync(decimal left, decimal bottom, decimal right, decimal top)
        {
            if (left is < 0 or > 360 || right is < 0 or > 360 || bottom > top || bottom is < -90 or > 90 ||
                top is < -90 or > 90)
                throw new BadRequestException(ErrorStrings.BadCoordinates);
            var res = await _waterSourcePlaceService.GetInRectangleAsync(left, bottom, right, top);
            return Ok(_mapper.Map<List<WaterSourcePlaceListingWithContributionDto>>(res));
        }
    }
}
