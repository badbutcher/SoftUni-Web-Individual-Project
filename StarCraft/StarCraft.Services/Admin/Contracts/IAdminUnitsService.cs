namespace StarCraft.Services.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;

    public interface IAdminUnitsService
    {
        Dictionary<Race, List<string>> GetAllBuildings();

        Task CreateUnitAsync(string name, Race race, int mineralCost, int gasCost, int health, int damage, string building, byte[] image);

        bool DoesUnitExists(string name, Race race);
    }
}