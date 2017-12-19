namespace StarCraft.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Web.Areas.Admin.Models.Users;
    using StarCraft.Web.Infrastructure.Extensions;
    using static StarCraft.Services.ServiceConstants;

    public class UsersController : AdminBaseController
    {
        private readonly IAdminUsersService users;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUsersService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllUsers(int page = FirstPage)
        {
            return this.View(new UserListingViewModel
            {
                Users = await this.users.AllAsync(page),
                TotalUsers = await this.users.TotalAsync(),
                CurrentPage = page
            });
        }

        public async Task<IActionResult> AddUserResources(string userId, int minerals, int gas)
        {
            User user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData.AddErrorMessage("User not found.");
                return this.RedirectToAction(nameof(this.AllUsers));
            }

            if (minerals <= 0)
            {
                minerals = 0;
            }

            if (gas <= 0)
            {
                gas = 0;
            }

            await this.users.AddResourcesAsync(userId, minerals, gas);

            return this.RedirectToAction(nameof(this.AllUsers));
        }
    }
}