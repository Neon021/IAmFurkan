using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Blog.Helpers.Startup
{
    public static class AdminRoleInit
    {
        public static async Task<bool> InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            Task task = Task.Run(async () =>
            {
                string adminRole = "Admin";
                string adminUserName = configuration["AdminCredentials:UserName"];
                string adminEmail = configuration["AdminCredentials:Email"];
                string adminPassword = configuration["AdminCredentials:Password"];

                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    IdentityUser adminUser = new()
                    {
                        UserName = adminUserName,
                        Email = adminEmail
                    };

                    IdentityResult result = await userManager.CreateAsync(adminUser, adminPassword);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                    }
                }
            });

            await task;
            return task.IsCompletedSuccessfully;
        }
    }
}
