namespace StarCraft.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web.Areas.Admin.Models.Units;
    using StarCraft.Web.Controllers;
    using StarCraft.Web.Infrastructure.Extensions;

    public class UnitsController : AdminBaseController
    {
        private readonly IAdminUnitsService units;

        public UnitsController(IAdminUnitsService units)
        {
            this.units = units;
        }

        public async Task<IActionResult> AllUnits()
        {
            var result = await this.units.AllUnitsAsync();

            return this.View(result);
        }

        public async Task<IActionResult> CreateUnit()
        {
            var buildings = await this.units.GetAllBuildingsFormAsync();

            return this.View(new CreateUnitModel
            {
                Buildings = buildings
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit(CreateUnitModel unitModel, IFormFile image)
        {
            bool exists = await this.units.DoesUnitExistsAsync(unitModel.Name, unitModel.Race);

            if (!exists)
            {
                ModelState.AddModelError(string.Empty, $"The unit {unitModel.Name} for the race {unitModel.Race} exists.");
            }

            if (!ModelState.IsValid)
            {
                unitModel.Buildings = await this.units.GetAllBuildingsFormAsync();
                return this.View(unitModel);
            }

            var fileContents = await image.ToByteArrayAsync();

            if (!image.FileName.EndsWith(".png") || image.Length > DataConstants.MaxByteImageSize)
            {
                return this.View(nameof(this.CreateUnit));
            }

            await this.units.CreateUnitAsync(
                unitModel.Name,
                unitModel.Race,
                unitModel.UnlockLevel,
                unitModel.ExpWorth,
                unitModel.MineralCost,
                unitModel.GasCost,
                unitModel.Health,
                unitModel.Damage,
                unitModel.Building,
                fileContents);

            TempData.AddSuccessMessage($"Unit {unitModel.Name} created successfully!");

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var unit = await this.units.FindByIdAsync(id);

            if (unit == null)
            {
                ModelState.AddModelError(string.Empty, $"The unit was not found.");
                return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
            }

            return this.View(new EditUnitModel
            {
                Name = unit.Name,
                ExpWorth = unit.ExpWorth,
                UnlockLevel = unit.UnlockLevel,
                MineralCost = unit.MineralCost,
                GasCost = unit.GasCost,
                Health = unit.Health,
                Damage = unit.Damage
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditUnitModel model, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var unit = this.units.FindByIdAsync(id);

            if (unit == null)
            {
                ModelState.AddModelError(string.Empty, $"The unit was not found.");
                return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
            }

            var fileContents = await image.ToByteArrayAsync();

            if (!image.FileName.EndsWith(".png") || image.Length > DataConstants.MaxByteImageSize)
            {
                return this.View(nameof(this.CreateUnit));
            }

            await this.units.EditAsync(id, model.Name, model.ExpWorth, model.UnlockLevel, model.MineralCost, model.GasCost, model.Health, model.Damage, fileContents);

            return this.RedirectToAction(nameof(this.AllUnits));
        }
    }
}