namespace StarCraft.Services.Admin.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using StarCraft.Data.Models;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Admin.Contracts;
    using StarCraft.Web.Data;

    public class AdminBuildingsService : IAdminBuildingsService
    {
        private readonly StarCraftDbContext db;

        public AdminBuildingsService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task CreateBuilding(string name, Race race, int mineralCost, int gasCost)
        {
            Building building = new Building
            {
                Name = name,
                Race = race,
                MineralCost = mineralCost,
                GasCost = gasCost
            };

            this.db.Buildings.Add(building);
            await this.db.SaveChangesAsync();
        }

        public bool DoesBuildingExists(string name, Race race)
        {
            Building building = this.db.Buildings.FirstOrDefault(a => a.Name.ToLower() == name.ToLower() && a.Race == race);

            if (building == null)
            {
                return true;
            }

            return false;
        }
    }
}
