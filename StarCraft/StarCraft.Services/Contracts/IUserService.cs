namespace StarCraft.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraft.Services.Models;

    public interface IUserService
    {
        Task<Tuple<bool, string>> BuyBuildingAsync(int buildingId, string userId);

        Task<Tuple<bool, string>> BuyUnitAsync(int unitId, string userId, int quantity);

        Task<UserInfoBattleServiceModel> FindRandomPlayerAsync(string userId);

        Task<BattleResultServiceModel> BattleEnemyAsync(string userId, string enemyId);

        Task<IEnumerable<UnitBasicStatsServiceModel>> GetUserUnitsAsync(string id);

        void UpdateUserResources(string id, int level);
    }
}