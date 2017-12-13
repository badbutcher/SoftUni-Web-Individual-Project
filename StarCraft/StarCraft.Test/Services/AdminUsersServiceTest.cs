namespace StarCraft.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Admin.Implementations;
    using StarCraft.Web.Data;
    using Xunit;

    public class AdminUsersServiceTest
    {
        public AdminUsersServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncCorrectCount()
        {
            // Arrange
            AdminUsersService adminUsersService = await this.DataBaseInitialize();

            // Act
            var result = await adminUsersService.AllAsync();

            //Assert
            result.Should().HaveCount(6);
        }

        [Fact]
        public async Task AllAsyncCorrectOrderCorrectCount()
        {
            // Arrange
            AdminUsersService adminUsersService = await this.DataBaseInitialize();

            // Act
            var result = await adminUsersService.AllAsync();

            //Assert
            result.Should().HaveCount(6);
        }

        [Fact]
        public async Task AddResourcesAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());

            await db.SaveChangesAsync();

            AdminUsersService adminUsersService = new AdminUsersService(db);

            User user = new User
            {
                Id = "1",
                Minerals = 150,
                Gas = 200
            };

            // Act
            await adminUsersService.AddResourcesAsync("1", 150, 200);
            var result = await db.Users
                .Select(a => new
                {
                    a.Id,
                    a.Minerals,
                    a.Gas
                })
                .FirstOrDefaultAsync(a => a.Id == "1");

            //Assert
            result.ShouldBeEquivalentTo(user);
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<User> UserTestData()
        {
            return new List<User>()
            {
                new User() { Id = "1", Race = Race.Terran, UserName = "UserName1", Email = "Email1" },
                new User() { Id = "3", Race = Race.Zerg, UserName = "UserName2", Email = "Email2" },
                new User() { Id = "5", Race = Race.Protoss, UserName = "UserName3", Email = "Email3" },
                new User() { Id = "2", Race = Race.Terran, UserName = "UserName4", Email = "Email4" },
                new User() { Id = "4", Race = Race.Zerg, UserName = "UserName5", Email = "Email5" },
                new User() { Id = "6", Race = Race.Protoss, UserName = "UserName6", Email = "Email6" }
            };
        }

        private async Task<AdminUsersService> DataBaseInitialize()
        {
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());

            await db.SaveChangesAsync();

            AdminUsersService adminUsersService = new AdminUsersService(db);
            return adminUsersService;
        }
    }
}