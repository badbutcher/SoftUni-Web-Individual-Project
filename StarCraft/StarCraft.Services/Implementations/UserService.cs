namespace StarCraft.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Hangfire;
    using Microsoft.EntityFrameworkCore;
    using StarCraft.Data;
    using StarCraft.Data.Models;
    using StarCraft.Services.Contracts;
    using StarCraft.Services.Models;
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

            if (user.Race != building.Race)
            {
                return this.BadRequest();
            }

            if (user.Level < building.UnlockLevel)
            {
                return this.BadRequest();
            }

            var usersBuildings = this.db.UserBuilding.Where(a => a.UserId == userId);

            if (usersBuildings.Any(a => a.BuildingId == buildingId))
            {
                return this.BadRequest();
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
            if (quantity <= 0)
            {
                quantity = 1;
            }

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

            if (!(user.Race != unit.Race || user.Level > unit.UnlockLevel))
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

            UnitUser unitQuantity = await this.db.FindAsync<UnitUser>(unitId, userId);
            var userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();

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

        public async Task<IEnumerable<UnitBasicStatsServiceModel>> GetUserUnitsAsync(string userId)
        {
            List<UnitBasicStatsServiceModel> units = new List<UnitBasicStatsServiceModel>();
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);
            var userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();
            foreach (var u in userUnits)
            {
                Unit unit = await this.db.Units.FirstOrDefaultAsync(a => a.Id == u.UnitId);
                units.Add(new UnitBasicStatsServiceModel
                {
                    Quantity = u.Quantity,
                    Image = unit.Image
                });
            }

            return units;
        }

        public async Task<UserInfoBattleServiceModel> FindRandomPlayerAsync(string userId)
        {
            Random r = new Random();
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);
            List<UnitUser> userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();
            await this.GetUnits(user.Units);

            if (user.Units.Sum(a => a.Quantity) <= 0)
            {
                return null;
            }

            var enemies = await this.db.Users
                .ProjectTo<UserInfoBattleServiceModel>()
                .OrderBy(a => a.ArmyQuantity)
                .Where(a => a.Race != user.Race &&
                a.Level >= user.Level - 1 &&
                a.Level <= user.Level + 1 &&
                a.ArmyQuantity >= 1)
                .Take(50)
                .ToListAsync();

            if (enemies.Count == 0)
            {
                return null;
            }

            var enemy = enemies[r.Next(0, enemies.Count)];

            return enemy;
        }

        public async Task<UserInfoBattleServiceModel> UserBattleInfoAsync(string userId)
        {
            UserInfoBattleServiceModel user = await this.db.Users
                .ProjectTo<UserInfoBattleServiceModel>()
                .FirstOrDefaultAsync(a => a.Id == userId);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<BattleResultServiceModel> BattleEnemyAsync(string userId, string enemyId)
        {
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            if (user == null)
            {
                return null;
            }

            User enemy = await this.db.Users.FirstOrDefaultAsync(a => a.Id == enemyId);

            if (enemy == null)
            {
                return null;
            }

            List<UnitUser> userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();
            List<UnitUser> enemyUnits = await this.db.Users.Where(a => a.Id == enemy.Id).Select(c => c.Units).FirstOrDefaultAsync();

            await this.GetUnits(userUnits);
            await this.GetUnits(enemyUnits);
            var battle = await this.StartBattle(userUnits, enemyUnits);

            battle.UserMineralsWon += battle.EnemyTroopsLost.Sum(a => a.Value) * enemy.Level * user.Level;
            battle.EnemyMineralsWon += battle.UserTroopsLost.Sum(a => a.Value) * user.Level * enemy.Level;
            battle.UserGasWon += battle.EnemyTroopsLost.Sum(a => a.Value) * (enemy.Level + user.Level);
            battle.EnemyGasWon += battle.EnemyTroopsLost.Sum(a => a.Value) * (user.Level + enemy.Level);

            user.CurrentExp += battle.UserXpWon;
            enemy.CurrentExp += battle.EnemyXpWon;
            user.Minerals += battle.EnemyTroopsLost.Sum(a => a.Value) * enemy.Level * user.Level;
            enemy.Minerals += battle.UserTroopsLost.Sum(a => a.Value) * user.Level * enemy.Level;
            user.Gas += battle.EnemyTroopsLost.Sum(a => a.Value) * (enemy.Level + user.Level);
            enemy.Gas += battle.EnemyTroopsLost.Sum(a => a.Value) * (user.Level + enemy.Level);

            await this.LevelUp(user);
            await this.LevelUp(enemy);
            await this.db.SaveChangesAsync();

            return battle;
        }

        public void UpdateUserResources(string id, int level)
        {
            var user = this.db.Users.FirstOrDefault(a => a.Id == id);
            user.Minerals += 25 * level;
            user.Gas += 5 * level;

            this.db.SaveChanges();
        }

        private async Task<BattleResultServiceModel> StartBattle(List<UnitUser> user, List<UnitUser> enemy)
        {
            BattleResultServiceModel result = new BattleResultServiceModel();

            foreach (var userUnit in user.Where(a => a.Quantity >= 1))
            {
                foreach (var enemyUnit in enemy.Where(a => a.Quantity >= 1))
                {
                    int currentUserUnitDamage = userUnit.Unit.Damage * userUnit.Quantity;
                    int currentEnemyUnitDamage = enemyUnit.Unit.Damage * enemyUnit.Quantity;

                    int currentUserUnitCount = userUnit.Quantity;
                    int currentEnemyUnitCount = enemyUnit.Quantity;

                    int userUnitsToLose = (int)Math.Round((double)currentEnemyUnitDamage / userUnit.Unit.Health);
                    int enemyUnitsToLose = (int)Math.Round((double)currentUserUnitDamage / enemyUnit.Unit.Health);

                    if (userUnitsToLose >= currentUserUnitCount)
                    {
                        userUnitsToLose = currentUserUnitCount;
                    }

                    if (enemyUnitsToLose >= currentEnemyUnitCount)
                    {
                        enemyUnitsToLose = currentEnemyUnitCount;
                    }

                    this.AddLostUnits(result.UserTroopsLost, userUnit, userUnitsToLose);
                    this.AddLostUnits(result.EnemyTroopsLost, enemyUnit, enemyUnitsToLose);

                    userUnit.Quantity -= userUnitsToLose;
                    enemyUnit.Quantity -= enemyUnitsToLose;

                    result.UserXpWon += enemyUnitsToLose * enemyUnit.Unit.ExpWorth;
                    result.EnemyXpWon += userUnitsToLose * userUnit.Unit.ExpWorth;

                    if (userUnit.Quantity <= 0)
                    {
                        userUnit.Quantity = 0;
                        await this.db.SaveChangesAsync();
                        break;
                    }

                    await this.db.SaveChangesAsync();
                }
            }

            return result;
        }

        private void AddLostUnits(IDictionary<string, int> troopsLost, UnitUser unit, int unitsLost)
        {
            if (!troopsLost.ContainsKey(unit.Unit.Name))
            {
                troopsLost.Add(unit.Unit.Name, 0);
            }

            troopsLost[unit.Unit.Name] += unitsLost;
        }

        private async Task LevelUp(User user)
        {
            if (typeof(ServiceConstants).GetField("ExpForLevel" + user.Level) != null)
            {
                int value = (int)typeof(ServiceConstants).GetField("ExpForLevel" + user.Level).GetValue(null);
                if (user.CurrentExp >= value)
                {
                    user.CurrentExp = 0;
                    user.Level++;
                    RecurringJob.AddOrUpdate(user.Id, () => this.UpdateUserResources(user.Id, user.Level), Cron.Minutely);
                    await this.db.SaveChangesAsync();
                }
            }
        }

        private async Task GetUnits(List<UnitUser> userUnits)
        {
            foreach (var unit in userUnits)
            {
                Unit userUnit = await this.db.Units.FirstOrDefaultAsync(a => a.Id == unit.UnitId);
            }
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