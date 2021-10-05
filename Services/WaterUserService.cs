using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

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
            await _manager.CreateAsync(user, password);
            await _manager.AddToRoleAsync(user, "User");
        }
    }
}
