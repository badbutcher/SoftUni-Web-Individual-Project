namespace StarCraft.Web.Infrastructure.Seeds
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;

    public static class UsersSeed
    {
        public static IApplicationBuilder SeedUsers(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    for (int i = 1; i < 11; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            User user = new User
                            {
                                UserName = $"UserName{i}{j}",
                                Email = $"Email@Email{i}{j}",
                                Race = (Race)(j / 2),
                                Minerals = 5000000,
                                Gas = 5000000,
                                Level = i,
                                CurrentExp = 0
                            };

                            if (user.Race == Race.Terran)
                            {
                                user.Units.Add(new UnitUser() { UnitId = 3, UserId = user.Id, Quantity = (j + 4) * 3 });
                                user.Units.Add(new UnitUser() { UnitId = 5, UserId = user.Id, Quantity = (j + 3) * 2 });
                                user.Units.Add(new UnitUser() { UnitId = 6, UserId = user.Id, Quantity = j + 2 });
                            }
                            else if (user.Race == Race.Zerg)
                            {
                                user.Units.Add(new UnitUser() { UnitId = 12, UserId = user.Id, Quantity = (j + 4) * 3 });
                                user.Units.Add(new UnitUser() { UnitId = 14, UserId = user.Id, Quantity = (j + 3) * 2 });
                                user.Units.Add(new UnitUser() { UnitId = 17, UserId = user.Id, Quantity = j + 2 });
                            }
                            else if (user.Race == Race.Protoss)
                            {
                                user.Units.Add(new UnitUser() { UnitId = 21, UserId = user.Id, Quantity = (j + 4) * 3 });
                                user.Units.Add(new UnitUser() { UnitId = 23, UserId = user.Id, Quantity = (j + 3) * 2 });
                                user.Units.Add(new UnitUser() { UnitId = 26, UserId = user.Id, Quantity = j + 2 });
                            }

                            IdentityResult result = await userManager.CreateAsync(user, $"UserName{i}{j}");
                        }
                    }
                }).Wait();
            }

            return app;
        }
    }
}