namespace StarCraft.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web.Areas.Admin.Models.Buildings;
    using StarCraft.Web.Controllers;
    using StarCraft.Web.Infrastructure.Extensions;
    using static WebConstants;

    public class BuildingsController : AdminBaseController
    {
        private readonly IAdminBuildingsService buildings;

        public BuildingsController(IAdminBuildingsService buildings)
        {
            this.buildings = buildings;
        }

        public async Task<IActionResult> AllBuildings()
        {
            var result = await this.buildings.AllBuildingsAsync();

            return this.View(result);
        }

        public IActionResult CreateBuilding()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBuilding(CreateBuildingModel buildingsModel, IFormFile image)
        {
            bool exists = await this.buildings.DoesBuildingExistsAsync(buildingsModel.Name);

            if (exists)
            {
                ModelState.AddModelError(string.Empty, $"The building {buildingsModel.Name} for the race {buildingsModel.Race} exists.");
                return this.View(buildingsModel);
            }

            if (!ModelState.IsValid)
            {
                return this.View(buildingsModel);
            }

            byte[] fileContents = await image.ToByteArrayAsync();

            if (!image.FileName.EndsWith(ImageFormat) || image.Length > DataConstants.MaxByteImageSize)
            {
                return this.View(nameof(this.CreateBuilding));
            }

            await this.buildings.CreateBuildingAsync(
                buildingsModel.Name,
                buildingsModel.Race,
                buildingsModel.UnlockLevel,
                buildingsModel.MineralCost,
                buildingsModel.GasCost,
                fileContents);

            TempData.AddSuccessMessage($"Building {buildingsModel.Name} created successfully!");

            return this.RedirectToAction(nameof(HomeController.Index), HomeControllerName, new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var building = await this.buildings.FindByIdAsync(id);

            if (building == null)
            {
                return NullBuildingGoToHome();
            }

            return this.View(new BuildingServiceModel
            {
                Name = building.Name,
                MineralCost = building.MineralCost,
                GasCost = building.GasCost,
                Image = building.Image
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BuildingServiceModel model, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(Edit));
            }

            var building = await this.buildings.FindByIdAsync(id);

            if (building == null)
            {
                return NullBuildingGoToHome();
            }

            var found = await this.buildings.DoesBuildingExistsAsync(model.Name);

            if (found)
            {
                TempData.AddErrorMessage(BuildingExistsMessage);
                return this.RedirectToAction(nameof(HomeController.Index), HomeControllerName, new { area = string.Empty });
            }

            byte[] fileContents = await image.ToByteArrayAsync();

            if (!image.FileName.EndsWith(ImageFormat) || image.Length > DataConstants.MaxByteImageSize)
            {
                return this.View(nameof(this.CreateBuilding));
            }

            await this.buildings.EditAsync(id, model.Name, model.MineralCost, model.GasCost, fileContents);

            return this.RedirectToAction(nameof(this.AllBuildings));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var building = await this.buildings.FindByIdAsync(id);

            if (building == null)
            {
                return NullBuildingGoToHome();
            }

            return this.View(new DeleteBuildingModel
            {
                Id = id,
                Building = new BuildingServiceModel
                {
                    Name = building.Name,
                    MineralCost = building.MineralCost,
                    GasCost = building.GasCost,
                    Image = building.Image
                }
            });
        }

        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var building = await this.buildings.FindByIdAsync(id);

            if (building == null)
            {
                return NullBuildingGoToHome();
            }

            bool delete = await this.buildings.DeleteAsync(id);

            if (!delete)
            {
                ModelState.AddModelError(string.Empty, BuildingNotFoundMessage);
                return this.RedirectToAction(nameof(HomeController.Index), HomeControllerName, new { area = string.Empty });
            }

            return this.RedirectToAction(nameof(this.AllBuildings));
        }

        private IActionResult NullBuildingGoToHome()
        {
            TempData.AddErrorMessage(BuildingNotFoundMessage);
            return this.RedirectToAction(nameof(HomeController.Index), HomeControllerName, new { area = string.Empty });
        }
    }
}