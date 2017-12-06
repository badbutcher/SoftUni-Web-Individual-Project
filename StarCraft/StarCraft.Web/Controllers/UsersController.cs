namespace StarCraft.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web.Infrastructure.Extensions;
    using StarCraft.Web.Models.Buildings;
    using StarCraft.Web.Models.UsersViewModels;

    [Authorize]
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

            var buildings = await this.buildings.AllBuildingsAsync(user.Id, user.Level, user.Race);

            return this.View(new UserBuyBuildingsViewModel
            {
                Buildings = buildings,
                Minerals = user.Minerals,
                Gas = user.Gas
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyBuilding(int buildingId)
        {
            var userId = this.userManager.GetUserId(User);

            Tuple<bool, string> success = await this.users.BuyBuildingAsync(buildingId, userId);

            if (!success.Item1)
            {
                TempData.AddErrorMessage(success.Item2);
                return this.RedirectToAction(nameof(UsersController.BuyBuilding));
            }

            TempData.AddSuccessMessage(success.Item2);

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> BuyUnit()
        {
            var user = await this.userManager.FindByNameAsync(User.Identity.Name);

            var units = await this.units.AllUnitsAsync(user.Id, user.Race);
            var userUnits = await this.users.GetUserUnits(user.Id);

            return this.View(new UserBuyUnitsViewModel
            {
                Units = units,
                Minerals = user.Minerals,
                Gas = user.Gas,
                BoughtUnits = userUnits
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyUnit(int unitId, int quantity)
        {
            var userId = this.userManager.GetUserId(User);

            Tuple<bool, string> success = await this.users.BuyUnitAsync(unitId, userId, quantity);

            if (!success.Item1)
            {
                TempData.AddErrorMessage(success.Item2);
                return this.RedirectToAction(nameof(UsersController.BuyUnit));
            }

            TempData.AddSuccessMessage(success.Item2);

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> FindRandomPlayer()
        {
            var userId = this.userManager.GetUserId(User);

            var enemy = await this.users.FindRandomPlayer(userId);

            if (enemy == null)
            {
                TempData.AddErrorMessage("No enemies found for you. Please try again later.");
                return this.View(new UserInfoBattleServiceModel
                {
                    ArmyQuantity = 0
                });
            }

            return this.View(enemy);
        }

        public async Task<IActionResult> BattleEnemy(string enemyId)
        {
            var userId = this.userManager.GetUserId(User);

            await this.users.BattleEnemy(userId, enemyId);

            return this.View();
        }

        public IActionResult Home()
        {
            return this.View();
        }
    }
}