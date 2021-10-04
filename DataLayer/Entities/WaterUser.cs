using System;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class WaterUser : IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public WaterUser()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
