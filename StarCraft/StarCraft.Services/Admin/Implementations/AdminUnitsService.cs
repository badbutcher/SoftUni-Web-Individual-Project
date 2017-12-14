namespace StarCraft.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web.Data;

    public class AdminUnitsService : IAdminUnitsService
    {
        private readonly StarCraftDbContext db;

        public AdminUnitsService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BasicUnitInfoServiceModel>> AllUnitsAsync()
        {
            var result = await this.db.Units
               .OrderBy(a => a.Race)
               .ProjectTo<BasicUnitInfoServiceModel>()
               .ToListAsync();

            return result;
        }

        public async Task CreateUnitAsync(string name, Race race, int unlockLevel, int expWorth, int mineralCost, int gasCost, int health, int damage, string building, byte[] image)
        {
            Building buildingExists = await this.db.Buildings.FirstOrDefaultAsync(a => a.Name == building);

            if (buildingExists == null)
            {
                return;
            }

            Unit unit = new Unit
            {
                Name = name,
                Race = race,
                UnlockLevel = unlockLevel,
                ExpWorth = expWorth,
                MineralCost = mineralCost,
                GasCost = gasCost,
                Health = health,
                Damage = damage,
                Image = image,
            };

            unit.Buildings.Add(new BuildingUnit { BuildingId = buildingExists.Id, UnitId = unit.Id });

            this.db.Units.Add(unit);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Unit unit = await this.db.Units.FirstOrDefaultAsync(a => a.Id == id);

            this.db.Units.Remove(unit);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DoesUnitExistsAsync(string name, Race race)
        {
            Unit unit = await this.db.Units.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower() && a.Race == race);

            if (unit == null)
            {
                return true;
            }

            return false;
        }

        public async Task EditAsync(int id, string name, int expWorth, int unlockLevel, int mineralCost, int gasCost, int health, int damage, byte[] image)
        {
            Unit exists = await this.db.Units.FindAsync(id);

            if (exists == null)
            {
                return;
            }

            exists.Name = name;
            exists.ExpWorth = expWorth;
            exists.UnlockLevel = unlockLevel;
            exists.MineralCost = mineralCost;
            exists.GasCost = gasCost;
            exists.Health = health;
            exists.Damage = damage;
            exists.Image = image;

            await this.db.SaveChangesAsync();
        }

        public async Task<UnitServiceModel> FindByIdAsync(int id)
        {
            var result = await this.db.Units
               .Where(c => c.Id == id)
               .ProjectTo<UnitServiceModel>()
               .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Dictionary<Race, List<string>>> GetAllBuildingsFormAsync()
        {
            Dictionary<Race, List<string>> buildings = new Dictionary<Race, List<string>>();

            buildings.Add(Race.Terran, await this.db.Buildings.Where(a => a.Race == Race.Terran).Select(c => c.Name).ToListAsync());
            buildings.Add(Race.Zerg, await this.db.Buildings.Where(a => a.Race == Race.Zerg).Select(c => c.Name).ToListAsync());
            buildings.Add(Race.Protoss, await this.db.Buildings.Where(a => a.Race == Race.Protoss).Select(c => c.Name).ToListAsync());
            /// TODOTODO: is there a better way?

            return buildings;
        }
    }
}