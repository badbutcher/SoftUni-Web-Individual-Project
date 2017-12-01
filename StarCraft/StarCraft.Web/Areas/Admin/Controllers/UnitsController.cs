namespace StarCraft.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using StarCraft.Data;
    using StarCraft.Services.Admin.Contracts;
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

        public IActionResult CreateUnit()
        {
            var buildings = this.units.GetAllBuildings();

            return this.View(new CreateUnitModel
            {
                Buildings = buildings
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit(CreateUnitModel unitModel, IFormFile image)
        {
            bool exists = this.units.DoesUnitExists(unitModel.Name, unitModel.Race);

            if (!exists)
            {
                ModelState.AddModelError(string.Empty, $"The unit {unitModel.Name} for the race {unitModel.Race} exists.");
            }

            if (!ModelState.IsValid)
            {
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
                unitModel.MineralCost,
                unitModel.GasCost,
                unitModel.Health,
                unitModel.Damage,
                unitModel.Building,
                fileContents);

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}