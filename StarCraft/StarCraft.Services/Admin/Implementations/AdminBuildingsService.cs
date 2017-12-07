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

    public class AdminBuildingsService : IAdminBuildingsService
    {
        private readonly StarCraftDbContext db;

        public AdminBuildingsService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BasicBuildingInfoServiceModel>> AllBuildingsAsync()
        {
            var result = await this.db.Buildings
                .OrderBy(a => a.Race)
                .ProjectTo<BasicBuildingInfoServiceModel>()
                .ToListAsync();

            return result;
        }

        public async Task CreateBuildingAsync(string name, Race race, int unlockLevel, int mineralCost, int gasCost, byte[] image)
        {
            Building building = new Building
            {
                Name = name,
                Race = race,
                UnlockLevel = unlockLevel,
                MineralCost = mineralCost,
                GasCost = gasCost,
                Image = image
            };

            this.db.Buildings.Add(building);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DoesBuildingExistsAsync(string name, Race race)
        {
            Building building = await this.db.Buildings.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower() && a.Race == race);

            if (building == null)
            {
                return true;
            }

            return false;
        }

        public async Task EditAsync(int id, string name, int mineralCost, int gasCost, byte[] image)
        {
            Building exists = await this.db.Buildings.FindAsync(id);

            if (exists == null)
            {
                return;
            }

            exists.Name = name;
            exists.MineralCost = mineralCost;
            exists.GasCost = gasCost;
            exists.Image = image;

            await this.db.SaveChangesAsync();
        }

        public async Task<EditBuildingModel> FindByIdAsync(int id)
        {
            var result = await this.db.Buildings
               .Where(c => c.Id == id)
               .ProjectTo<EditBuildingModel>()
               .FirstOrDefaultAsync();

            return result;
        }
    }
}