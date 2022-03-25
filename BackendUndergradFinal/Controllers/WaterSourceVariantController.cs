using AutoMapper;
using Communication.SourceVariantDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

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
