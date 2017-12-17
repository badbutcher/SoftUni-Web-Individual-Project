namespace StarCraft.Web.Infrastructure.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using StarCraft.Data.Models;
    using StarCraft.Web.Data;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<StarCraftDbContext>().Database.Migrate();

                UserManager<User> userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    string adminName = WebConstants.AdministratorRole;

                    string[] roles = new[]
                    {
                        adminName
                    };

                    foreach (var role in roles)
                    {
                        bool roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }

                    string adminEmail = "admin@admin.com";

                    User adminUser = await userManager.FindByEmailAsync(adminEmail);

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = adminName
                        };

                        await userManager.CreateAsync(adminUser, "admin12");

                        await userManager.AddToRoleAsync(adminUser, adminName);
                    }
                }).Wait();
            }

            return app;
        }
    }
}