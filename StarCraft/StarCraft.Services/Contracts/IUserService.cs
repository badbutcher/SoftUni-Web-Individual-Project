namespace StarCraft.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task BuyBuilding(int buildingId, string userId);

        Task BuyUnit(int unitId, string userId, int quantity);
    }
}