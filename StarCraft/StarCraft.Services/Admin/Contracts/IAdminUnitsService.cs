namespace StarCraft.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Models;

    public interface IAdminUnitsService
    {
        Task<Dictionary<Race, List<string>>> GetAllBuildingsFormAsync();

        Task CreateUnitAsync(string name, Race race, int unlockLevel, int expWorth, int mineralCost, int gasCost, int health, int damage, string building, byte[] image);

        Task<bool> DoesUnitExistsAsync(string name, Race race);

        Task<IEnumerable<BasicUnitInfoServiceModel>> AllUnitsAsync();

        Task<UnitServiceModel> FindByIdAsync(int id);

        Task EditAsync(int id, string name, int expWorth, int unlockLevel, int mineralCost, int gasCost, int health, int damage, byte[] fileContents);

        Task DeleteAsync(int id);
    }
}