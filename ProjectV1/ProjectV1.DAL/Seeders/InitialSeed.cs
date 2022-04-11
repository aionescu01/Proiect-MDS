using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectV1.DAL.Entities.Auth;
using ProjectV1.DAL.Enums;

namespace ProjectV1.DAL.Seeders
{
    public class InitialSeed
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;

        public InitialSeed(RoleManager<Role> roleManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async void SeedRoles()
        {
            if (_context.Roles.Any())
            {
                return;
            }

            var roles = Enum.GetValues(typeof(RolesEnum));

            foreach (var role in roles)
            {
                var roleName = Enum.GetName(typeof(RolesEnum), role);
                var roleToAdd = new Role(roleName);
                _roleManager.CreateAsync(roleToAdd).Wait();
            }

        }
    }
}
