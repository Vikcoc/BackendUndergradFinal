using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Communication;
using Communication.AccountDto;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;

namespace BackendUndergradFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerWithError
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

        [HttpPost("sign_in")]
        public async Task<IActionResult> SignInAsync(UserSignInDto userDto)
        {
            return Ok(await _user.SignInAsync(userDto.Email, userDto.Password));
        }
    }
}
