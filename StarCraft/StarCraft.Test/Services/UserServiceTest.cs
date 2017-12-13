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
    using StarCraft.Services.Implementations;
    using StarCraft.Web.Data;
    using Xunit;

    public class UserServiceTest
    {
        public UserServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task BuyBuildingAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BuyBuildingAsync(1, "1");

            // Assert
            result.Item1.ShouldBeEquivalentTo(true);
            result.Item2.ShouldBeEquivalentTo("Building Bunker bought successfully!");
        }

        [Fact]
        public async Task BuyBuildingAsyncNotEnoughMinerals()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BuyBuildingAsync(2, "1");

            // Assert
            result.Item1.ShouldBeEquivalentTo(false);
            result.Item2.ShouldBeEquivalentTo("We require more minerals.");
        }

        [Fact]
        public async Task BuyBuildingAsyncNotEnoughGas()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BuyBuildingAsync(7, "1");

            // Assert
            result.Item1.ShouldBeEquivalentTo(false);
            result.Item2.ShouldBeEquivalentTo("We require more vespene gas.");
        }

        [Fact]
        public async Task BuyBuildingAsyncUserSpendsResources()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            await userService.BuyBuildingAsync(1, "1");
            var result = await db.Users.FirstOrDefaultAsync(a => a.Id == "1");

            // Assert
            result.Minerals.Should().Be(900);
            result.Gas.Should().Be(950);
        }

        [Fact]
        public async Task BuyUnitAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BuyUnitAsync(1, "1", 5);

            // Assert
            result.Item1.ShouldBeEquivalentTo(true);
            result.Item2.ShouldBeEquivalentTo("5 unit/s of type SCV bought successfully!");
        }

        [Fact]
        public async Task BuyUnitAsyncQuantityCorrect()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            await userService.BuyUnitAsync(1, "1", 5);
            var result = await db.Users.FirstOrDefaultAsync(a => a.Id == "1");

            // Assert
            result.Units.Sum(a => a.Quantity).Should().Be(5);
        }

        [Fact]
        public async Task BuyUnitAsyncNotEnoughMinerals()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BuyUnitAsync(1, "1", 25);

            // Assert
            result.Item1.ShouldBeEquivalentTo(false);
            result.Item2.ShouldBeEquivalentTo("We require more minerals.");
        }

        [Fact]
        public async Task BuyUnitAsyncNotEnoughGas()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.BuildingsData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BuyUnitAsync(7, "1", 25);

            // Assert
            result.Item1.ShouldBeEquivalentTo(false);
            result.Item2.ShouldBeEquivalentTo("We require more vespene gas.");
        }

        [Fact]
        public async Task GetUserUnitsAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.BuildingsData());
            await db.AddRangeAsync(this.UserUnitTestData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.GetUserUnitsAsync("1");

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task BattleEnemyAsyncSuccess()
        {
            // Arrange
            StarCraftDbContext db = this.GetDatabase();

            await db.AddRangeAsync(this.UserTestData());
            await db.AddRangeAsync(this.UnitTestData());
            await db.AddRangeAsync(this.UserUnitTestData());

            await db.SaveChangesAsync();

            UserService userService = new UserService(db);

            // Act
            var result = await userService.BattleEnemyAsync("1", "2");

            // Assert
            result.UserXpWon.Should().Be(10);
            result.EnemyXpWon.Should().Be(40);
            result.UserTroopsLost.Sum(a => a.Value).Should().Be(8);
            result.EnemyTroopsLost.Sum(a => a.Value).Should().Be(2);
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<UserBuilding> UserBuildingsTestData()
        {
            return new List<UserBuilding>()
            {
                new UserBuilding() { BuildingId = 1, UserId = "1" },
                new UserBuilding() { BuildingId = 2, UserId = "2" },
                new UserBuilding() { BuildingId = 3, UserId = "3" },
                new UserBuilding() { BuildingId = 4, UserId = "4" },
                new UserBuilding() { BuildingId = 5, UserId = "5" },
                new UserBuilding() { BuildingId = 6, UserId = "6" },
            };
        }

        private IEnumerable<UnitUser> UserUnitTestData()
        {
            return new List<UnitUser>()
            {
                new UnitUser() { UnitId = 1, UserId = "1", Quantity = 15 },
                new UnitUser() { UnitId = 2, UserId = "1", Quantity = 15 },
                new UnitUser() { UnitId = 3, UserId = "2", Quantity = 15 },
                new UnitUser() { UnitId = 4, UserId = "2", Quantity = 15 },
                new UnitUser() { UnitId = 5, UserId = "5", Quantity = 15 },
                new UnitUser() { UnitId = 6, UserId = "6", Quantity = 15 },
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
                new Building() { Id = 1, Race = Race.Terran, Name = "Bunker", UnlockLevel = 0, MineralCost = 100, GasCost = 50 },
                new Building() { Id = 3, Race = Race.Zerg, Name = "Creep tumor", UnlockLevel = 0, MineralCost = 75, GasCost = 0 },
                new Building() { Id = 5, Race = Race.Protoss, Name = "Pylon", UnlockLevel = 0, MineralCost = 100, GasCost = 0 },
                new Building() { Id = 2, Race = Race.Terran, Name = "Barracks", UnlockLevel = 0, MineralCost = 1500, GasCost = 100 },
                new Building() { Id = 4, Race = Race.Zerg, Name = "Lair", UnlockLevel = 0, MineralCost = 400, GasCost = 100 },
                new Building() { Id = 6, Race = Race.Protoss, Name = "Nexus", UnlockLevel = 0, MineralCost = 400, GasCost = 50 },
                new Building() { Id = 7, Race = Race.Terran, Name = "CC", UnlockLevel = 0, MineralCost = 50, GasCost = 11111 }
            };
        }

        private IEnumerable<Unit> UnitTestData()
        {
            return new List<Unit>()
            {
                new Unit() { Id = 1, Race = Race.Terran, Name = "SCV", UnlockLevel = 0, MineralCost = 100, GasCost = 0, Damage = 6, Health = 50, ExpWorth = 5 },
                new Unit() { Id = 3, Race = Race.Zerg, Name = "Drone", UnlockLevel = 0, MineralCost = 75, GasCost = 0, Damage = 7, Health = 80, ExpWorth = 5 },
                new Unit() { Id = 5, Race = Race.Protoss, Name = "Probe", UnlockLevel = 0, MineralCost = 100, GasCost = 0, Damage = 2, Health = 90, ExpWorth = 5 },
                new Unit() { Id = 2, Race = Race.Terran, Name = "Marine", UnlockLevel = 0, MineralCost = 150, GasCost = 100, Damage = 5, Health = 75, ExpWorth = 5 },
                new Unit() { Id = 4, Race = Race.Zerg, Name = "Zergling", UnlockLevel = 0, MineralCost = 400, GasCost = 100, Damage = 9, Health = 160, ExpWorth = 5 },
                new Unit() { Id = 6, Race = Race.Protoss, Name = "Zealot", UnlockLevel = 0, MineralCost = 400, GasCost = 50, Damage = 10, Health = 50, ExpWorth = 5 },
                new Unit() { Id = 7, Race = Race.Terran, Name = "Raven", UnlockLevel = 0, MineralCost = 5, GasCost = 125, Damage = 8, Health = 60, ExpWorth = 5 }
            };
        }

        private IEnumerable<User> UserTestData()
        {
            return new List<User>()
            {
                new User() { Id = "1", Minerals = 1000, Gas = 1000, Level = 1, Race = Race.Terran, UserName = "UserName1", Email = "Email1" },
                new User() { Id = "3", Minerals = 1000, Gas = 1000, Level = 1, Race = Race.Zerg, UserName = "UserName2", Email = "Email2" },
                new User() { Id = "5", Minerals = 1000, Gas = 1000, Level = 1, Race = Race.Protoss, UserName = "UserName3", Email = "Email3" },
                new User() { Id = "2", Minerals = 1000, Gas = 1000, Level = 1, Race = Race.Terran, UserName = "UserName4", Email = "Email4" },
                new User() { Id = "4", Minerals = 1000, Gas = 1000, Level = 1, Race = Race.Zerg, UserName = "UserName5", Email = "Email5" },
                new User() { Id = "6", Minerals = 1000, Gas = 1000, Level = 1, Race = Race.Protoss, UserName = "UserName6", Email = "Email6" }
            };
        }
    }
}