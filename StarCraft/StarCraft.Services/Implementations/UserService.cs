namespace StarCraft.Services.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using StarCraft.Data.Models;
    using StarCraft.Services.Contracts;
    using StarCraft.Web.Data;

    public class UserService : IUserService
    {
        private readonly StarCraftDbContext db;

        public UserService(StarCraftDbContext db)
        {
            this.db = db;
        }

        public async Task BuyBuilding(int buildingId, string userId)
        {
            User user = this.db.Users.FirstOrDefault(a => a.Id == userId);

            if (user == null)
            {
                return;
            }

            Building building = this.db.Buildings.FirstOrDefault(a => a.Id == buildingId);

            if (building == null)
            {
                return;
            }

            if (user.Buildings.FirstOrDefault(a => a.BuildingId == buildingId) != null)
            {
                return;
            }

            if (!(user.Minerals >= building.MineralCost && user.Gas >= building.GasCost))
            {
                return;
            }

            user.Minerals -= building.MineralCost;
            user.Gas -= building.GasCost;
            user.Buildings.Add(new UserBuilding { BuildingId = buildingId, UserId = userId });
            await this.db.SaveChangesAsync();
        }

        public async Task BuyUnit(int unitId, string userId, int quantity)
        {
            User user = this.db.Users.FirstOrDefault(a => a.Id == userId);

            if (user == null)
            {
                return;
            }

            Unit unit = this.db.Units.FirstOrDefault(a => a.Id == unitId);

            if (unit == null)
            {
                return;
            }

            if (!(user.Minerals >= unit.MineralCost && user.Gas >= unit.GasCost))
            {
                return;
            }

            var unitQuantity = await this.db.FindAsync<UnitUser>(unitId, userId);
            var userUnits = this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefault();

            if (!userUnits.Any() || unitQuantity == null)
            {
                user.Units.Add(new UnitUser { UnitId = unitId, UserId = userId });
                await this.db.SaveChangesAsync();
                unitQuantity = await this.db.FindAsync<UnitUser>(unitId, userId);
            }

            user.Minerals -= unit.MineralCost * quantity;
            user.Gas -= unit.GasCost * quantity;
            unitQuantity.Quantity += quantity;

            await this.db.SaveChangesAsync();
        }
    }
}