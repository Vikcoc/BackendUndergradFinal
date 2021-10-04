using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<WaterUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration config)
        {
            _logger = logger;
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
                new[]{new Claim(ClaimTypes.Role, "Admin")},
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
        [Authorize(Roles = "Boris")]
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
