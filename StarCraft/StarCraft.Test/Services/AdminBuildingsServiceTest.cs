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

    public class AdminBuildingsServiceTest
    {
        public AdminBuildingsServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllBuildingsAsyncCorrectCount()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.TestData());

            await db.SaveChangesAsync();

            AdminBuildingsService adminBuildingsService = new AdminBuildingsService(db);

            // Act
            var result = await adminBuildingsService.AllBuildingsAsync();

            // Assert
            result.Should().HaveCount(6);
        }

        [Fact]
        public async Task AllBuildingsAsyncCorrectRaceOrder()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.TestData());

            await db.SaveChangesAsync();

            AdminBuildingsService adminBuildingsService = new AdminBuildingsService(db);

            // Act
            var result = await adminBuildingsService.AllBuildingsAsync();

            // Assert
            result.Should()
                .Match(a => a.ElementAt(0).Race == Race.Terran &&
                a.ElementAt(2).Race == Race.Zerg &&
                a.ElementAt(5).Race == Race.Protoss);
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<Building> TestData()
        {
            return new List<Building>()
            {
                new Building() { Id = 1, Race = Race.Terran, Name = "Bunker", UnlockLevel = 1, MineralCost = 100, GasCost = 0 },
                new Building() { Id = 3, Race = Race.Zerg, Name = "Creep tumor", UnlockLevel = 3, MineralCost = 75, GasCost = 0 },
                new Building() { Id = 5, Race = Race.Protoss, Name = "Pylon", UnlockLevel = 5, MineralCost = 100, GasCost = 0 },
                new Building() { Id = 2, Race = Race.Terran, Name = "Barracks", UnlockLevel = 2, MineralCost = 150, GasCost = 100 },
                new Building() { Id = 4, Race = Race.Zerg, Name = "Lair", UnlockLevel = 4, MineralCost = 400, GasCost = 100 },
                new Building() { Id = 6, Race = Race.Protoss, Name = "Nexus", UnlockLevel = 6, MineralCost = 400, GasCost = 50 }
            };
        }
    }
}