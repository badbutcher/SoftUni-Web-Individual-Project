namespace StarCraft.Services.Admin.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;

    public interface IAdminBuildingsService
    {
        Task CreateBuilding(string name, Race race, int mineralCost, int gasCost);

        bool DoesBuildingExists(string name, Race race);
    }
}
