namespace StarCraft.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services.Contracts;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly IBuildingService buildings;
        private readonly UserManager<User> userManager;

        public UsersController(IUserService users, IBuildingService buildings, UserManager<User> userManager)
        {
            this.users = users;
            this.buildings = buildings;
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

            return this.RedirectToAction(nameof(HomeController.Index));
        }
    }
}