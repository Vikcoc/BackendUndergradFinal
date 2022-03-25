using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace BackendUndergradFinal.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly UserManager<WaterUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private IConfiguration _config;


        public WeatherForecastController(UserManager<WaterUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                new[] { new Claim(ClaimTypes.Role, "Admin") },
                expires: DateTime.Now.AddDays(120),
                signingCredentials: credentials);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpGet("auth")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get2()
        {
            return Ok();
        }

        [HttpGet("auth2")]
        [Authorize(Roles = "User")]
        public IActionResult Get3()
        {
            return Ok();
        }

        [HttpGet("path")]
        public IActionResult Get4()
        {
            return Ok(Directory.GetCurrentDirectory());
        }
        [HttpGet("reflections")]
        public IActionResult Get5()
        {
            return Ok();
        }
    }
}
