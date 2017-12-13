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

    public class UnitServiceTest
    {
        public UnitServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllUnitsAsyncCorrectCount()
        {
            // Arrange
            UnitService unitService = await this.DataBaseInitialize();

            // Act
            var result = await unitService.AllUnitsAsync("1", Race.Terran);

            // Assert
            result.Should().HaveCount(2);
        }

        private async Task<UnitService> DataBaseInitialize()
        {
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.BuildingsData());
            await db.AddRangeAsync(this.UserBuildingsTestData());
            await db.AddRangeAsync(this.BuildingUnitTestData());

            await db.SaveChangesAsync();

            UnitService unitService = new UnitService(db);
            return unitService;
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<User> UserTestData()
        {
            return new List<User>()
            {
                new User() { Id = "1", Level = 1, Race = Race.Terran, UserName = "UserName1", Email = "Email1" },
                new User() { Id = "3", Level = 1, Race = Race.Zerg, UserName = "UserName2", Email = "Email2" },
                new User() { Id = "5", Level = 1, Race = Race.Protoss, UserName = "UserName3", Email = "Email3" },
                new User() { Id = "2", Level = 1, Race = Race.Terran, UserName = "UserName4", Email = "Email4" },
                new User() { Id = "4", Level = 1, Race = Race.Zerg, UserName = "UserName5", Email = "Email5" },
                new User() { Id = "6", Level = 1, Race = Race.Protoss, UserName = "UserName6", Email = "Email6" }
            };
        }

        private IEnumerable<UserBuilding> UserBuildingsTestData()
        {
            return new List<UserBuilding>()
            {
                new UserBuilding() { BuildingId = 1, UserId = "1" },
                new UserBuilding() { BuildingId = 2, UserId = "1" },
                new UserBuilding() { BuildingId = 3, UserId = "3" },
                new UserBuilding() { BuildingId = 4, UserId = "4" },
                new UserBuilding() { BuildingId = 5, UserId = "5" },
                new UserBuilding() { BuildingId = 6, UserId = "6" },
            };
        }

        private IEnumerable<BuildingUnit> BuildingUnitTestData()
        {
            return new List<BuildingUnit>()
            {
                new BuildingUnit() { BuildingId = 1, UnitId = 1 },
                new BuildingUnit() { BuildingId = 2, UnitId = 2 },
                new BuildingUnit() { BuildingId = 3, UnitId = 3 },
                new BuildingUnit() { BuildingId = 4, UnitId = 4 },
                new BuildingUnit() { BuildingId = 5, UnitId = 5 },
                new BuildingUnit() { BuildingId = 6, UnitId = 6 },
            };
        }

        private IEnumerable<Building> BuildingsData()
        {
            return new List<Building>()
            {
                new Building() { Id = 1, Race = Race.Terran, Name = "Bunker", UnlockLevel = 0, MineralCost = 100, GasCost = 0 },
                new Building() { Id = 3, Race = Race.Zerg, Name = "Creep tumor", UnlockLevel = 0, MineralCost = 75, GasCost = 0 },
                new Building() { Id = 5, Race = Race.Protoss, Name = "Pylon", UnlockLevel = 0, MineralCost = 100, GasCost = 0 },
                new Building() { Id = 2, Race = Race.Terran, Name = "Barracks", UnlockLevel = 0, MineralCost = 150, GasCost = 100 },
                new Building() { Id = 4, Race = Race.Zerg, Name = "Lair", UnlockLevel = 0, MineralCost = 400, GasCost = 100 },
                new Building() { Id = 6, Race = Race.Protoss, Name = "Nexus", UnlockLevel = 0, MineralCost = 400, GasCost = 50 }
            };
        }

        private IEnumerable<Unit> UnitTestData()
        {
            return new List<Unit>()
            {
                new Unit() { Id = 1, Race = Race.Terran, Name = "SCV", UnlockLevel = 0, MineralCost = 100, GasCost = 0, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 3, Race = Race.Zerg, Name = "Drone", UnlockLevel = 0, MineralCost = 75, GasCost = 0, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 5, Race = Race.Protoss, Name = "Probe", UnlockLevel = 0, MineralCost = 100, GasCost = 0, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 2, Race = Race.Terran, Name = "Marine", UnlockLevel = 0, MineralCost = 150, GasCost = 100, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 4, Race = Race.Zerg, Name = "Zergling", UnlockLevel = 0, MineralCost = 400, GasCost = 100, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 6, Race = Race.Protoss, Name = "Zealot", UnlockLevel = 0, MineralCost = 400, GasCost = 50, Damage = 5, Health = 5, ExpWorth = 5 }
            };
        }
    }
}