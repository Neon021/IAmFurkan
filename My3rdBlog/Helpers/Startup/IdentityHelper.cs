using Blog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Helpers.Startup
{
    public static class IdentityHelper
    {
        public static async Task<bool> InitAsync(IWebHost host)
        {
            bool res = true;

            Task task = Task.Run(async () =>
            {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                try
                {
                    var dbCtx = services.GetRequiredService<AppDbContext>();
                    if (await dbCtx.Database.CanConnectAsync())
                    {
                        if ((await dbCtx.Database.GetPendingMigrationsAsync()).Any())
                        {
                            await dbCtx.Database.MigrateAsync();
                        }
                    }
                    else
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError("Cannot connect to the database.");
                    }

                    res = await AdminRoleInit.InitializeAsync(
                        services.GetRequiredService<UserManager<IdentityUser>>(),
                        services.GetRequiredService<RoleManager<IdentityRole>>(),
                        services.GetRequiredService<IConfiguration>());
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during application startup.");
                }
            });

            await task;
            return res && task.IsCompletedSuccessfully;
        }
    }
}
