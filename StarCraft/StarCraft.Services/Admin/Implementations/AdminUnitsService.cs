namespace StarCraft.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Web.Data;

    public class AdminUnitsService : IAdminUnitsService
    {
        private readonly StarCraftDbContext db;

        public AdminUnitsService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task CreateUnitAsync(string name, Race race, int mineralCost, int gasCost, int health, int damage, string building, byte[] image)
        {
            var buildingExists = this.db.Buildings.FirstOrDefault(a => a.Name == building);

            if (buildingExists == null)
            {
                return;
            }

            Unit unit = new Unit
            {
                Name = name,
                Race = race,
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

        public bool DoesUnitExists(string name, Race race)
        {
            Unit unit = this.db.Units.FirstOrDefault(a => a.Name.ToLower() == name.ToLower() && a.Race == race);

            if (unit == null)
            {
                return true;
            }

            return false;
        }

        public Dictionary<Race, List<string>> GetAllBuildings()
        {
            var buildings = new Dictionary<Race, List<string>>();

            buildings.Add(Race.Terran, this.db.Buildings.Where(a => a.Race == Race.Terran).Select(c => c.Name).ToList());
            buildings.Add(Race.Zerg, this.db.Buildings.Where(a => a.Race == Race.Zerg).Select(c => c.Name).ToList());
            buildings.Add(Race.Protoss, this.db.Buildings.Where(a => a.Race == Race.Protoss).Select(c => c.Name).ToList());
            /// TODO: is there a better way?

            return buildings;
        }
    }
}