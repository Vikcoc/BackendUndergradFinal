using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Exceptions;

namespace Services
{
    public class WaterUserService
    {
        private readonly UserManager<WaterUser> _manager;

        public WaterUserService(UserManager<WaterUser> manager)
        {
            _manager = manager;
        }

        public async Task CreateUserAsync(WaterUser user, string password)
        {
            user.UserName = user.Email;
            var result = await _manager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                if (result.Errors.Select(e => e.Code)
                    .Any(e => new List<string> {"DuplicateUserName", "DuplicateEmail"}.Contains(e)))
                    throw new BadRequestException(ErrorStrings.EmailAlreadyInUse);
                throw new Exception(result.Errors.FirstOrDefault()?.Code);
            }
            await _manager.AddToRoleAsync(user, "User");
        }
    }
}
