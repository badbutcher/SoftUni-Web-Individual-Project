namespace StarCraft.Test.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web;
    using StarCraft.Web.Areas.Admin.Controllers;
    using StarCraft.Web.Areas.Admin.Models.Buildings;
    using StarCraft.Web.Data;
    using Xunit;

    public class BuildingsControllerTest
    {
        [Fact]
        public void BuildingsControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(BuildingsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void BuildingsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(BuildingsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }

        [Fact]
        public void CreateBuildingReturnsCorrectView()
        {
            // Arrange
            var adminBuildingsService = new Mock<IAdminBuildingsService>();
            adminBuildingsService.Setup(a => a.AllBuildingsAsync());
            var controller = new BuildingsController(adminBuildingsService.Object);

            // Act
            var result = controller.CreateBuilding();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task AllBuildingsReturnsCorrectCount()
        {
            // Arrange
            var adminBuildingsService = new Mock<IAdminBuildingsService>();
            adminBuildingsService.Setup(a => a.AllBuildingsAsync()).Returns(Task.FromResult(this.TestData()));
            var controller = new BuildingsController(adminBuildingsService.Object);

            // Act
            var result = await controller.AllBuildings();

            // Assert
            result.Should().BeOfType<ViewResult>();

            var viewResult = result.As<ViewResult>();
            viewResult.ViewData.Model.As<IEnumerable<BasicBuildingInfoServiceModel>>().Should().HaveCount(3);

            //var model = Assert.IsAssignableFrom<IEnumerable<BasicBuildingInfoServiceModel>>(viewResult.ViewData.Model);
            //model.Should().HaveCount(3);
        }

        [Fact]
        public async Task CreateBuildingSuccess()
        {
            // Arrange
            const string NameValue = "Factory";
            const Race RaceValue = Race.Terran;
            const int UnlockLevelValue = 1;
            const int MineralCostValue = 150;
            const int GasCostValue = 50;
            byte[] imageValue = new byte[100];

            string modelName = null;
            Race modelRace = Race.Terran;
            int modelUnlockLevel = 0;
            int modelMineralCost = 0;
            int modelGasCost = 0;
            byte[] modelImage = null;
            string successMessage = null;

            var fileMock = new Mock<IFormFile>();
            var fileName = "testImage.png";
            var lenght = 1024;
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(a => a.OpenReadStream()).Returns(ms);
            fileMock.Setup(a => a.FileName).Returns(fileName);
            fileMock.Setup(a => a.Length).Returns(lenght);

            var file = fileMock.Object;

            var adminBuildingsService = new Mock<IAdminBuildingsService>();
            adminBuildingsService
                .Setup(a => a.CreateBuildingAsync(
                    It.IsAny<string>(),
                    It.IsAny<Race>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<byte[]>()))
                    .Callback((string name, Race race, int unlockLevel, int mineralCost, int gasCost, byte[] image) =>
                    {
                        modelName = name;
                        modelRace = race;
                        modelUnlockLevel = unlockLevel;
                        modelMineralCost = mineralCost;
                        modelGasCost = gasCost;
                        modelImage = image;
                    }).Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new BuildingsController(adminBuildingsService.Object);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.CreateBuilding(new CreateBuildingModel
            {
                Name = NameValue,
                Race = RaceValue,
                UnlockLevel = UnlockLevelValue,
                MineralCost = MineralCostValue,
                GasCost = GasCostValue,
                Image = imageValue
            }, file);

            // Assert
            modelName.Should().Be(NameValue);
            modelRace.Should().Be(RaceValue);
            modelUnlockLevel.Should().Be(UnlockLevelValue);
            modelMineralCost.Should().Be(MineralCostValue);
            modelGasCost.Should().Be(GasCostValue);

            successMessage.Should().Be($"Building {NameValue} created successfully!");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Home");
            result.As<RedirectToActionResult>().RouteValues.Keys.Should().Contain("area");
            result.As<RedirectToActionResult>().RouteValues.Values.Should().Contain(string.Empty);
        }

        private StarCraftDbContext GetDatabase()
        {
            var databaseOptions = new DbContextOptionsBuilder<StarCraftDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new StarCraftDbContext(databaseOptions);
        }

        private IEnumerable<Building> TestDataBuildings()
        {
            var sessions = new List<Building>
            {
                new Building()
                {
                    Id = 1,
                    Name = "Factory",
                    Race = Race.Terran,
                    MineralCost = 100,
                    GasCost = 0,
                    Image = new byte[100]
                },
                new Building()
                {
                    Id = 2,
                    Name = "Spawning pool",
                    Race = Race.Zerg,
                    MineralCost = 200,
                    GasCost = 0,
                    Image = new byte[100]
                },
                new Building()
                {
                    Id = 3,
                    Name = "Nexus",
                    Race = Race.Protoss,
                    MineralCost = 200,
                    GasCost = 200,
                    Image = new byte[100]
                }
            };

            return sessions;
        }

        private IEnumerable<BasicBuildingInfoServiceModel> TestData()
        {
            var sessions = new List<BasicBuildingInfoServiceModel>
            {
                new BasicBuildingInfoServiceModel()
                {
                    Id = 1,
                    Name = "Bunker",
                    Race = Race.Terran,
                    MineralCost = 100,
                    GasCost = 0,
                    Image = new byte[100]
                },
                new BasicBuildingInfoServiceModel()
                {
                    Id = 2,
                    Name = "Spawning pool",
                    Race = Race.Zerg,
                    MineralCost = 200,
                    GasCost = 0,
                    Image = new byte[100]
                },
                new BasicBuildingInfoServiceModel()
                {
                    Id = 3,
                    Name = "Nexus",
                    Race = Race.Protoss,
                    MineralCost = 200,
                    GasCost = 200,
                    Image = new byte[100]
                }
            };

            return sessions;
        }
    }
}