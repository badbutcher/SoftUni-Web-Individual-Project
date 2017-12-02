namespace StarCraft.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services.Contracts;
    using StarCraft.Web.Infrastructure.Extensions;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IBuildingService buildings;
        private readonly IUnitService units;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users, IBuildingService buildings, IUnitService units, UserManager<User> userManager)
        {
            this.users = users;
            this.buildings = buildings;
            this.units = units;
            this.userManager = userManager;
        }

        public async Task<IActionResult> BuyBuilding()
        {
            var user = await this.userManager.FindByNameAsync(User.Identity.Name);

            var buildings = await this.buildings.AllBuildingsAsync(user.Id, user.Race);

            return this.View(buildings);
        }

        [HttpPost]
        public async Task<IActionResult> BuyBuilding(int buildingId)
        {
            var userId = this.userManager.GetUserId(User);

            await this.users.BuyBuilding(buildingId, userId);

            TempData.AddSuccessMessage($"Building bought successfully!");

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> BuyUnit()
        {
            var user = await this.userManager.FindByNameAsync(User.Identity.Name);

            var units = await this.units.AllUnitsAsync(user.Id, user.Race);

            return this.View(units);
        }

        [HttpPost]
        public async Task<IActionResult> BuyUnit(int unitId, int quantity)
        {
            var userId = this.userManager.GetUserId(User);

            await this.users.BuyUnit(unitId, userId, quantity);

            TempData.AddSuccessMessage($"{quantity} unit/s bought successfully!");

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}