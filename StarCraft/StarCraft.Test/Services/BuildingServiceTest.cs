namespace StarCraft.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Implementations;
    using StarCraft.Web.Data;
    using Xunit;

    public class BuildingServiceTest
    {
        public BuildingServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllBuildingsAsyncCorrectCount()
        {
            // Arrange
            BuildingService buildingService = await this.DataBaseInitialize();

            // Act
            var result = await buildingService.AllBuildingsAsync("1", 1, Race.Terran);

            // Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task AllBuildingsAsyncRace()
        {
            // Arrange
            BuildingService buildingService = await this.DataBaseInitialize();

            // Act
            var result = await buildingService.AllBuildingsAsync("1", 1, Race.Zerg);

            // Assert
            result.Should().HaveCount(0);
        }

        private async Task<BuildingService> DataBaseInitialize()
        {
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            BuildingService buildingService = new BuildingService(db);
            return buildingService;
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<Building> BuildingsData()
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