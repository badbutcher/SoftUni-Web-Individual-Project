namespace StarCraft.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Models;

    public interface IAdminBuildingsService
    {
        Task CreateBuildingAsync(string name, Race race, int unlockLevel, int mineralCost, int gasCost, byte[] image);

        Task<bool> DoesBuildingExistsAsync(string name);

        Task<BuildingServiceModel> FindByIdAsync(int id);

        Task<IEnumerable<BasicBuildingInfoServiceModel>> AllBuildingsAsync();

        Task EditAsync(int id, string name, int mineralCost, int gasCost, byte[] image);

        Task<bool> DeleteAsync(int id);
    }
}