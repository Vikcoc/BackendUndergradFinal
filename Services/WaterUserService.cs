using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WaterUserService
    {
        private readonly UserManager<WaterUser> _manager;
        private readonly SignInManager<WaterUser> _signIn;
        private readonly IConfiguration _config;

        public WaterUserService(UserManager<WaterUser> manager, IConfiguration config, SignInManager<WaterUser> signIn)
        {
            _manager = manager;
            _config = config;
            _signIn = signIn;
        }

        public async Task CreateUserAsync(WaterUser user, string password)
        {
            user.UserName = user.Email;
            var result = await _manager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                if (result.Errors.Select(e => e.Code)
                    .Any(e => new List<string> { "DuplicateUserName", "DuplicateEmail" }.Contains(e)))
                    throw new BadRequestException(ErrorStrings.EmailAlreadyInUse);
                throw new Exception(result.Errors.Select(x => x.Description).Aggregate((x, y) => x + "\n" + y));
            }
            var result2 = await _manager.AddToRoleAsync(user, "User");
            if (!result2.Succeeded)
            {
                throw new Exception(result2.Errors.Select(x => x.Description).Aggregate((x, y) => x + "\n" + y));
            }
        }

        public async Task<string> SignInAsync(string email, string password)
        {

            var user = await _manager.FindByEmailAsync(email);
            if (user == null)
                throw new BadRequestException(ErrorStrings.LoginFail);

            var res = await _signIn.PasswordSignInAsync(user, password, false, false);

            if (res.Succeeded)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                var roles = await _manager.GetRolesAsync(user);
                var claims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(10),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);

            }

            throw new BadRequestException(ErrorStrings.LoginFail);
        }
    }
}
