using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataLayer
{
    public class MyAppUser : IdentityUser<Guid>
    {
        public string Test { get; set; }
    }
}
