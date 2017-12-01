namespace StarCraft.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Contracts;
    using StarCraft.Services.Models;
    using StarCraft.Web.Data;

    public class BuildingService : IBuildingService
    {
        private readonly StarCraftDbContext db;

        public BuildingService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BasicBuildingInfoServiceModel>> AllBuildingsAsync(string userId, Race userRace)
        {
            var buildings = await this.db.Buildings
                .Where(r => r.Race == userRace)
                .Where(a => a.Users.All(c => !c.UserId.Equals(userId)))
                .ProjectTo<BasicBuildingInfoServiceModel>()
                .ToListAsync();

            return buildings;
        }
    }
}