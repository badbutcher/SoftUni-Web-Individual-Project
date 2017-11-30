namespace StarCraft.Services.Admin.Contracts
{
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;

    public interface IAdminBuildingsService
    {
        Task CreateBuilding(string name, Race race, int mineralCost, int gasCost, byte[] image);

        bool DoesBuildingExists(string name, Race race);
    }
}