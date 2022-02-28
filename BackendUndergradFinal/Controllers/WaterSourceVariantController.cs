using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Communication.SourceVariantDto;
using Microsoft.AspNetCore.Authorization;
using Services;

namespace BackendUndergradFinal.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WaterSourceVariantController : ControllerWithError
    {
        private readonly WaterSourceVariantService _sourceVariant;
        private readonly IMapper _mapper;

        public WaterSourceVariantController(WaterSourceVariantService sourceVariant, IMapper mapper)
        {
            _sourceVariant = sourceVariant;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<WaterSourceVariantDto>>> GetWaterSourcesAsync()
        {
            var sources = await _sourceVariant.GetSourceVariantsWithImageAsync();
            return Ok(_mapper.Map<List<WaterSourceVariantDto>>(sources));
        }
    }
}
