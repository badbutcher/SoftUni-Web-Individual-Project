namespace StarCraft.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services;
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
            User user = await this.userManager.FindByNameAsync(User.Identity.Name);

            var buildings = await this.buildings.AllBuildingsAsync(user.Id, user.Level, user.Race);

            string currentExp = GetUserExp(user);

            return this.View(new UserBuyBuildingsViewModel
            {
                Buildings = buildings,
                Level = user.Level,
                CurrentExp = currentExp,
                Minerals = user.Minerals,
                Gas = user.Gas
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyBuilding(int buildingId)
        {
            string userId = this.userManager.GetUserId(User);

            Tuple<bool, string> success = await this.users.BuyBuildingAsync(buildingId, userId);

            if (!success.Item1)
            {
                TempData.AddErrorMessage(success.Item2);
                return this.RedirectToAction(nameof(UsersController.BuyBuilding));
            }

            TempData.AddSuccessMessage(success.Item2);

            return this.RedirectToAction(nameof(this.BuyBuilding));
        }

        public async Task<IActionResult> BuyUnit()
        {
            User user = await this.userManager.FindByNameAsync(User.Identity.Name);

            var units = await this.units.AllUnitsAsync(user.Id, user.Race);
            var userUnits = await this.users.GetUserUnitsAsync(user.Id);

            string currentExp = GetUserExp(user);

            return this.View(new UserBuyUnitsViewModel
            {
                Units = units,
                Level = user.Level,
                CurrentExp = currentExp,
                Minerals = user.Minerals,
                Gas = user.Gas,
                BoughtUnits = userUnits
            });
        }

        [HttpPost]
        public async Task<IActionResult> BuyUnit(int unitId, int quantity)
        {
            string userId = this.userManager.GetUserId(User);

            Tuple<bool, string> success = await this.users.BuyUnitAsync(unitId, userId, quantity);

            if (!success.Item1)
            {
                TempData.AddErrorMessage(success.Item2);
                return this.RedirectToAction(nameof(UsersController.BuyUnit));
            }

            TempData.AddSuccessMessage(success.Item2);

            return this.RedirectToAction(nameof(this.BuyUnit));
        }

        public async Task<IActionResult> FindRandomPlayer()
        {
            string userId = this.userManager.GetUserId(User);

            var enemy = await this.users.FindRandomPlayerAsync(userId);
            var user = await this.users.UserBattleInfoAsync(userId);

            if (enemy == null || user == null)
            {
                TempData.AddErrorMessage("No enemies found or you don't have enough troops.");

                return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
            }

            return this.View(new BattleServiceModel
            {
                User = user,
                Enemy = enemy
            });
        }

        public async Task<IActionResult> BattleEnemy(string enemyId)
        {
            string userId = this.userManager.GetUserId(User);

            var result = await this.users.BattleEnemyAsync(userId, enemyId);

            if (result == null)
            {
                TempData.AddErrorMessage("No enemies found or you don't have enough troops.");
                return this.RedirectToAction(nameof(this.FindRandomPlayer));
            }

            return this.View(result);
        }

        private static string GetUserExp(User user)
        {
            string currentExp = "Max level";
            if (typeof(ServiceConstants).GetField("ExpForLevel" + user.Level) != null)
            {
                currentExp = $"{user.CurrentExp}/{(int)typeof(ServiceConstants).GetField("ExpForLevel" + user.Level).GetValue(null)}";
            }

            return currentExp;
        }
    }
}