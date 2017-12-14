namespace StarCraft.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data.Models;
    using StarCraft.Services.Contracts;
    using StarCraft.Web.Models;

    public class HomeController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public HomeController(IUserService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = await this.userManager.GetUserAsync(User);
                return this.View(user.Race);
            }

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}