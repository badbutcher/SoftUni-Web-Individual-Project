namespace StarCraft.Services.Admin.Contracts
{
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Models;

    public interface IAdminBuildingsService
    {
        Task CreateBuildingAsync(string name, Race race, int mineralCost, int gasCost, byte[] image);

        bool DoesBuildingExists(string name, Race race);
    }
}