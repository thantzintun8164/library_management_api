using LibraryManagementSystem.Application.Settings;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystem.Infrastructure.Persistence.SeedData
{
    internal class UserSeeder : IEntitySeeder
    {

        private readonly DefaultUsersSettings _defaultUsersSettings;
        public UserSeeder(IOptions<DefaultUsersSettings> settings)
        {
            _defaultUsersSettings = settings.Value;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //if (!await roleManager.RoleExistsAsync(Role.Admin.ToString()))
            //    await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));

            //if (!await roleManager.RoleExistsAsync(Role.User.ToString()))
            //    await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));

            #region Admin Account
            if (await userManager.FindByEmailAsync(_defaultUsersSettings.AdminEmail) == null)
            {
                var adminAccount = new ApplicationUser()
                {
                    FullName = "Admin",
                    UserName = Guid.NewGuid().ToString(),
                    Email = _defaultUsersSettings.AdminEmail,
                    Gender = Gender.Male,
                    Role = Role.Admin
                };
                var result = await userManager.CreateAsync(adminAccount, _defaultUsersSettings.AdminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminAccount, Role.Admin.ToString());
            }
            #endregion

            #region User Account


            if (await userManager.FindByEmailAsync(_defaultUsersSettings.UserEmail) == null)
            {
                var userAccount = new ApplicationUser()
                {
                    FullName = "User",
                    UserName = Guid.NewGuid().ToString(),
                    Email = _defaultUsersSettings.UserEmail,
                    Gender = Gender.Male,
                    Role = Role.User
                };
                var result = await userManager.CreateAsync(userAccount, _defaultUsersSettings.UserPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(userAccount, Role.User.ToString());
            }
            #endregion

        }
    }
}
