namespace StarCraft.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Models;

    public interface IBuildingService
    {
        Task<IEnumerable<BasicBuildingInfoServiceModel>> AllBuildingsAsync(string userId, Race userRace);
    }
}