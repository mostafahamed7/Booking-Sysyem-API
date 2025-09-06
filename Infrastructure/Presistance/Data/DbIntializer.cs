using Domain.Contracts;
using Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;

namespace Presistance.Data
{
    public class DbIntializer : IDbIntializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbIntializer(ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitializeIdentityAsync()
        {
            // Seed Default Roles & Users

            // 1. Seed Role
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            // 2. Seed User & Assign Role
            if (!_userManager.Users.Any())
            {
                var adminUser = new User
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "01000000000",
                };
                var superAdminUser = new User
                {
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "01032222222",
                };
                await _userManager.CreateAsync(adminUser, "Pass#w0rd");
                await _userManager.CreateAsync(superAdminUser, "Pass#w0rd");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            }
        }

    }
}
