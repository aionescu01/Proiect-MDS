using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectV1.DAL.Entities.Auth
{
    public class Role : IdentityRole<int>
    {
        public Role() { }

        public Role(string roleName) : base(roleName) { }
    }
}
