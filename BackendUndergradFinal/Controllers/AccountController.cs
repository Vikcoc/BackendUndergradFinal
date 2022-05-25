using System;
using System.Security.Claims;
using AutoMapper;
using Communication.AccountDto;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services.Exceptions;

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
        public async Task<ActionResult<string>> SignUpAsync(UserSignUpDto userDto)
        {
            await _user.CreateUserAsync(_mapper.Map<WaterUser>(userDto), userDto.Password);
            return Ok(await _user.SignInAsync(userDto.Email, userDto.Password));
        }

        [HttpPost("sign_in")]
        public async Task<ActionResult<string>> SignInAsync(UserSignInDto userDto)
        {
            return Ok(await _user.SignInAsync(userDto.Email, userDto.Password));
        }

        [HttpGet("name")]
        [Authorize]
        public async Task<ActionResult<string>> GetNameAsync()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await _user.GetUserNameAsync(Guid.Parse(id)));
        }
    }
}
