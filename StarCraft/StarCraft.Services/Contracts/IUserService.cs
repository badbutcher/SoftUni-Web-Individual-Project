namespace StarCraft.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<Tuple<bool, string>> BuyBuildingAsync(int buildingId, string userId);

        Task<Tuple<bool, string>> BuyUnitAsync(int unitId, string userId, int quantity);

        Task FindRandomPlayer(string userId);
    }
}