namespace StarCraft.Test.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web;
    using StarCraft.Web.Areas.Admin.Controllers;
    using Xunit;

    public class UnitsControllerTest
    {
        [Fact]
        public void UnitsControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(UnitsController);

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
        public void UnitsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(UnitsController);

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
        public async Task CreateUnitReturnsCorrectView()
        {
            // Arrange
            var adminUnitsService = new Mock<IAdminUnitsService>();
            adminUnitsService.Setup(a => a.AllUnitsAsync());
            var controller = new UnitsController(adminUnitsService.Object);

            // Act
            var result = await controller.CreateUnit();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task AllUnitsReturnsCorrectCount()
        {
            // Arrange
            var adminUnitsService = new Mock<IAdminUnitsService>();
            adminUnitsService.Setup(a => a.AllUnitsAsync()).Returns(Task.FromResult(this.TestData()));
            var controller = new UnitsController(adminUnitsService.Object);

            // Act
            var result = await controller.AllUnits();

            // Assert
            result.Should().BeOfType<ViewResult>();

            var viewResult = result.As<ViewResult>();
            viewResult.ViewData.Model.As<IEnumerable<BasicUnitInfoServiceModel>>().Should().HaveCount(3);
        }

        private IEnumerable<BasicUnitInfoServiceModel> TestData()
        {
            var sessions = new List<BasicUnitInfoServiceModel>
            {
                new BasicUnitInfoServiceModel()
                {
                    Id = 1,
                    Name = "SVC",
                    Race = Race.Terran,
                    MineralCost = 100,
                    GasCost = 0,
                    Image = new byte[100]
                },
                new BasicUnitInfoServiceModel()
                {
                    Id = 2,
                    Name = "Drone",
                    Race = Race.Zerg,
                    MineralCost = 200,
                    GasCost = 0,
                    Image = new byte[100]
                },
                new BasicUnitInfoServiceModel()
                {
                    Id = 3,
                    Name = "Probe",
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