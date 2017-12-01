namespace StarCraft.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Models;

    public interface IUnitService
    {
        Task<IEnumerable<BasicUnitInfoServiceModel>> AllUnitsAsync(string userId, Race race);
    }
}