namespace StarCraft.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Web.Areas.Admin.Models.Users;

    public class UsersController : AdminBaseController
    {
        private readonly IAdminUsersService users;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUsersService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllUsers(int page = 1)
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
            await this.users.AddResourcesAsync(userId, minerals, gas);

            return this.RedirectToAction(nameof(this.AllUsers));
        }
    }
}