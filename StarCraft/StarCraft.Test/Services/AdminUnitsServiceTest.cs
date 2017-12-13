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
    using StarCraft.Services.Models;
    using StarCraft.Web.Data;
    using Xunit;

    public class AdminUnitsServiceTest
    {
        public AdminUnitsServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllUnitsAsyncCorrectCount()
        {
            // Arrange
            AdminUnitsService adminUnitsService = await this.DataBaseInitialize();

            // Act
            var result = await adminUnitsService.AllUnitsAsync();

            //Assert
            result.Should().HaveCount(6);
        }

        [Fact]
        public async Task AllUnitsAsyncCorrectRaceOrder()
        {
            // Arrange
            AdminUnitsService adminUnitsService = await this.DataBaseInitialize();

            // Act
            var result = await adminUnitsService.AllUnitsAsync();

            // Assert
            result.Should()
                .Match(a => a.ElementAt(0).Race == Race.Terran &&
                a.ElementAt(2).Race == Race.Zerg &&
                a.ElementAt(5).Race == Race.Protoss);
        }

        [Fact]
        public async Task CreateUnitAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.BuildingTestData());

            await db.SaveChangesAsync();

            AdminUnitsService adminUnitsService = new AdminUnitsService(db);
            AdminBuildingsService adminBuildingsService = new AdminBuildingsService(db);

            // Act
            string name = "Medic";
            Race race = Race.Terran;
            int unlockLevel = 2;
            int mineralCost = 100;
            int gasCost = 0;
            int damage = 5;
            int health = 5;
            int expWorth = 5;
            byte[] image = new byte[100];

            await adminUnitsService.CreateUnitAsync(name, race, unlockLevel, expWorth, mineralCost, gasCost, health, damage, "Bunker", image);

            // Assert
            db.Units.Should().HaveCount(1);
        }

        [Fact]
        public async Task DoesUnitExistsAsyncFount()
        {
            // Arrange
            AdminUnitsService adminUnitsService = await this.DataBaseInitialize();

            // Act
            var result = await adminUnitsService.DoesUnitExistsAsync("Drone", Race.Zerg);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DoesUnitExistsAsyncNotFount()
        {
            // Arrange
            AdminUnitsService adminUnitsService = await this.DataBaseInitialize();

            // Act
            var result = await adminUnitsService.DoesUnitExistsAsync("Drone", Race.Terran);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UnitTestData());

            await db.SaveChangesAsync();

            AdminUnitsService adminUnitsService = new AdminUnitsService(db);

            // Act
            await adminUnitsService.DeleteAsync(2);

            // Assert
            db.Units.Should().HaveCount(5);
        }

        [Fact]
        public async Task FindByIdAsyncSuccess()
        {
            // Arrange
            AdminUnitsService adminUnitsService = await this.DataBaseInitialize();

            // Act
            var result = await adminUnitsService.FindByIdAsync(2);

            // Assert
            result.Should().BeOfType<UnitServiceModel>();
        }

        [Fact]
        public async Task EditAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UnitTestData());

            await db.SaveChangesAsync();

            AdminUnitsService adminUnitsService = new AdminUnitsService(db);

            // Act
            await adminUnitsService.EditAsync(1, "NewName", 50, 5, 50, 50, 50, 50, new byte[1000]);

            var result = await db.Units.FirstOrDefaultAsync(a => a.Id == 1);

            //Assert
            db.Units.Should().Contain(result);
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<Unit> UnitTestData()
        {
            return new List<Unit>()
            {
                new Unit() { Id = 1, Race = Race.Terran, Name = "SCV", UnlockLevel = 1, MineralCost = 100, GasCost = 0, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 3, Race = Race.Zerg, Name = "Drone", UnlockLevel = 3, MineralCost = 75, GasCost = 0, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 5, Race = Race.Protoss, Name = "Probe", UnlockLevel = 5, MineralCost = 100, GasCost = 0, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 2, Race = Race.Terran, Name = "Marine", UnlockLevel = 2, MineralCost = 150, GasCost = 100, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 4, Race = Race.Zerg, Name = "Zergling", UnlockLevel = 4, MineralCost = 400, GasCost = 100, Damage = 5, Health = 5, ExpWorth = 5 },
                new Unit() { Id = 6, Race = Race.Protoss, Name = "Zealot", UnlockLevel = 6, MineralCost = 400, GasCost = 50, Damage = 5, Health = 5, ExpWorth = 5 }
            };
        }

        private IEnumerable<Building> BuildingTestData()
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

        private async Task<AdminUnitsService> DataBaseInitialize()
        {
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UnitTestData());

            await db.SaveChangesAsync();

            AdminUnitsService adminUnitsService = new AdminUnitsService(db);
            return adminUnitsService;
        }
    }
}