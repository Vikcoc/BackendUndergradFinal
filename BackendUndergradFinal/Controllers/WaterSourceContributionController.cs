using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Communication.SourceContributionDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BackendUndergradFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WaterSourceContributionController : ControllerBase
    {
        private readonly WaterSourceContributionService _waterSourceContributionService;
        private readonly IMapper _mapper;

        public WaterSourceContributionController(WaterSourceContributionService waterSourceContributionService, IMapper mapper)
        {
            _waterSourceContributionService = waterSourceContributionService;
            _mapper = mapper;
        }

        [HttpGet("mine/{skip:int}/{take:int}")]
        public async Task<ActionResult<List<WaterSourceContributionWithPlaceDto>>> GetMyContributionsAsync(int skip = 0, int take = 5)
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _waterSourceContributionService.GetLatestUserContributionsAsync(id, skip, take);
            return Ok(_mapper.Map<List<WaterSourceContributionWithPlaceDto>>(res));
        }
    }
}
