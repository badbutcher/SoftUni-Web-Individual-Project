namespace StarCraft.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<Tuple<bool, string>> BuyBuildingAsync(int buildingId, string userId)
        {
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            if (user == null)
            {
                return this.BadRequest();
            }

            Building building = await this.db.Buildings.FirstOrDefaultAsync(a => a.Id == buildingId);

            if (building == null)
            {
                return this.BadRequest();
            }

            if (user.Buildings.FirstOrDefault(a => a.BuildingId == buildingId) != null)
            {
                return this.BadRequest(); ///TODO ????
            }

            if (!(user.Minerals >= building.MineralCost))
            {
                return this.MoreMinerals();
            }
            else if (!(user.Gas >= building.GasCost))
            {
                return this.MoreGas();
            }

            user.Minerals -= building.MineralCost;
            user.Gas -= building.GasCost;
            user.Buildings.Add(new UserBuilding { BuildingId = buildingId, UserId = userId });
            await this.db.SaveChangesAsync();

            return new Tuple<bool, string>(true, $"Building {building.Name} bought successfully!");
        }

        public async Task<Tuple<bool, string>> BuyUnitAsync(int unitId, string userId, int quantity)
        {
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            if (user == null)
            {
                return this.BadRequest();
            }

            Unit unit = await this.db.Units.FirstOrDefaultAsync(a => a.Id == unitId);

            if (unit == null)
            {
                return this.BadRequest();
            }

            if (!(user.Minerals >= unit.MineralCost * quantity))
            {
                return this.MoreMinerals();
            }
            else if (!(user.Gas >= unit.GasCost * quantity))
            {
                return this.MoreGas();
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

            return new Tuple<bool, string>(true, $"{quantity} unit/s of type {unit.Name} bought successfully!");
        }

        public async Task FindRandomPlayer(string userId)
        {
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);
            var userUnits = this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).First();
            int playerTroopsCount = 0;
            int playerTroopsHealth = 0;
            int playerTroopsDamage = 0;

            foreach (var item in userUnits)
            {
                var unit = this.db.Units.FirstOrDefault(a => a.Id == item.UnitId);
                playerTroopsCount += item.Quantity;
                playerTroopsHealth += unit.Health * item.Quantity;
                playerTroopsDamage += unit.Damage * item.Quantity;
            }

            Console.WriteLine();

            var enemy = await this.db.Users.FirstOrDefaultAsync(a => a.Race != user.Race && 
            a.Units.Count >= user.Units.Count-1 &&
            a.Units.Count <= user.Units.Count+1);

            if (enemy == null)
            {
                enemy = await this.db.Users.FirstOrDefaultAsync(a => a.Race != user.Race);
            }
            
            //TODO
        }

        private Tuple<bool, string> BadRequest()
        {
            return new Tuple<bool, string>(false, "Bad Request 404.");
        }

        private Tuple<bool, string> MoreMinerals()
        {
            return new Tuple<bool, string>(false, "We require more minerals.");
        }

        private Tuple<bool, string> MoreGas()
        {
            return new Tuple<bool, string>(false, "We require more vespene gas.");
        }
    }
}