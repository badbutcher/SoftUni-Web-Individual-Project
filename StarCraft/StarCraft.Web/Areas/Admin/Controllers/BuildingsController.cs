namespace StarCraft.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Web.Areas.Admin.Models.Buildings;
    using StarCraft.Web.Controllers;
    using StarCraft.Web.Infrastructure.Extensions;

    [Area(WebConstats.AdminArea)]
    [Authorize(Roles = WebConstats.AdministratorRole)]
    public class BuildingsController : Controller
    {
        private readonly IAdminBuildingsService buildings;

        public BuildingsController(IAdminBuildingsService buildings)
        {
            this.buildings = buildings;
        }

        public IActionResult CreateBuilding()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBuilding(CreateBuildingModel buildingsModel, IFormFile image)
        {
            bool exists = this.buildings.DoesBuildingExists(buildingsModel.Name, buildingsModel.Race);

            if (!exists)
            {
                ModelState.AddModelError(string.Empty, $"The building {buildingsModel.Name} for the race {buildingsModel.Race} exists.");
            }

            if (!ModelState.IsValid)
            {
                return this.View(buildingsModel);
            }

            var fileContents = await image.ToByteArrayAsync();

            if (!image.FileName.EndsWith(".png") || image.Length > DataConstants.MaxByteImageSize)
            {
                return this.View(nameof(this.CreateBuilding));
            }

            await this.buildings.CreateBuilding(
                buildingsModel.Name,
                buildingsModel.Race,
                buildingsModel.MineralCost,
                buildingsModel.GasCost,
                fileContents);

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}