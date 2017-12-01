namespace StarCraft.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web.Data;

    public class UnitService : IUnitService
    {
        private readonly StarCraftDbContext db;

        public UnitService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BasicUnitInfoServiceModel>> AllUnitsAsync(string userId, Race race)
        {
            var user = this.db.Users.FirstOrDefault(a => a.Id == userId);

            List<int> userBuildings = await this.db.UserBuilding
                .Where(a => a.UserId == userId /*&& a.Building.Race == race*/)
                .Select(a => a.Building.Id).ToListAsync();

            var userUnits = await this.db.BuildingUnit
                .Where(a => userBuildings.Contains(a.BuildingId))
                .Select(d => new BasicUnitInfoServiceModel
                {
                    Id = d.UnitId,
                    Name = d.Unit.Name,
                    Race = d.Unit.Race,
                    GasCost = d.Unit.GasCost,
                    MineralCost = d.Unit.MineralCost,
                    Health = d.Unit.Health,
                    Damage = d.Unit.Damage,
                    Image = d.Unit.Image
                }).ToListAsync();

            return userUnits;
        }
    }
}