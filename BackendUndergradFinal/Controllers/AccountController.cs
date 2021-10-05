using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Communication;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Services;

namespace BackendUndergradFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly WaterUserService _user;
        private readonly IMapper _mapper;

        public AccountController(WaterUserService user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUpAsync(UserSignUpDto userDto)
        {
            await _user.CreateUserAsync(_mapper.Map<WaterUser>(userDto), userDto.Password);
            return Ok();
        }
    }
}
