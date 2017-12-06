namespace StarCraft.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
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

        public async Task<IEnumerable<UnitBasicStatsServiceModel>> GetUserUnits(string userId)
        {
            List<UnitBasicStatsServiceModel> units = new List<UnitBasicStatsServiceModel>();
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);
            var userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();
            foreach (var u in userUnits)
            {
                var unit = this.db.Units.FirstOrDefault(a => a.Id == u.UnitId);
                units.Add(new UnitBasicStatsServiceModel
                {
                    Quantity = u.Quantity,
                    Image = unit.Image
                });
            }

            return units;
        }

        public async Task<UserInfoBattleServiceModel> FindRandomPlayer(string userId)
        {
            Random r = new Random();
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

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

        public async Task BattleEnemy(string userId, string enemyId)
        {
            Random r = new Random();
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            User enemy = await this.db.Users.FirstOrDefaultAsync(a => a.Id == enemyId);

            List<UnitUser> userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();
            List<UnitUser> enemyUnits = await this.db.Users.Where(a => a.Id == enemy.Id).Select(c => c.Units).FirstOrDefaultAsync();

            await this.GetUnits(userUnits);
            await this.GetUnits(enemyUnits);
            var battle = this.StartBattle(userUnits, enemyUnits);
            user.CurrentExp += battle.Result.Item1;
            enemy.CurrentExp += battle.Result.Item2;
            await this.LevelUp(user);
            await this.LevelUp(enemy);
            await this.db.SaveChangesAsync();
        }

        private async Task LevelUp(User user)
        {
            if ((typeof(DataConstants)).GetField("ExpForLevel" + user.Level) != null)
            {
                int value = (int)typeof(DataConstants).GetField("ExpForLevel" + user.Level).GetValue(null);
                if (user.CurrentExp >= value)
                {
                    user.CurrentExp = 0;
                    user.Level++;
                    await this.db.SaveChangesAsync();
                }
            }
        }

        private async Task<Tuple<int, int>> StartBattle(List<UnitUser> attacker, List<UnitUser> defender)
        {
            int totalAttackerHealth = attacker.Sum(a => a.Unit.Health * a.Quantity);
            int totalAttackerDamage = attacker.Sum(a => a.Unit.Damage * a.Quantity);

            int totalDefenderHealth = defender.Sum(a => a.Unit.Health * a.Quantity);
            int totalDefenderDamage = defender.Sum(a => a.Unit.Damage * a.Quantity);

            ////totalAttackerHealth -= totalDefenderDamage;
            ////totalDefenderHealth -= totalAttackerDamage;
            int userXpWon = 0;
            int enemyXpWon = 0;

            foreach (var attackerUnit in attacker.Where(a => a.Quantity >= 1))
            {
                int maxHealth = attackerUnit.Unit.Health * attackerUnit.Quantity;
                int maxDmg = attackerUnit.Unit.Damage * attackerUnit.Quantity;
                var currentAttackingUnitHealth = attackerUnit.Unit.Health * attackerUnit.Quantity;
                var currentAttackingUnitDamage = attackerUnit.Unit.Damage * attackerUnit.Quantity;

                foreach (var defenderUnit in defender.Where(a => a.Quantity > 1))
                {
                    var currentDefendingUnitHealth = defenderUnit.Unit.Health * defenderUnit.Quantity;
                    var currentDefendingUnitDamage = defenderUnit.Unit.Damage * defenderUnit.Quantity;

                    currentAttackingUnitHealth -= currentDefendingUnitDamage;
                    currentDefendingUnitHealth -= currentAttackingUnitDamage;

                    var currentAttackingUnitCount = attackerUnit.Quantity;
                    var currentDefenderUnitCount = defenderUnit.Quantity;

                    attackerUnit.Quantity -= (int)Math.Ceiling((double)currentDefendingUnitDamage / attackerUnit.Unit.Health);
                    defenderUnit.Quantity -= (int)Math.Ceiling((double)currentAttackingUnitDamage / defenderUnit.Unit.Health);

                    userXpWon += (currentDefenderUnitCount - defenderUnit.Quantity) * defenderUnit.Unit.ExpWorth;
                    enemyXpWon += (currentAttackingUnitCount - attackerUnit.Quantity) * attackerUnit.Unit.ExpWorth;

                    if (attackerUnit.Quantity <= 0)
                    {
                        attackerUnit.Quantity = 0;
                        await this.db.SaveChangesAsync();
                        break;
                    }
                    else if (defenderUnit.Quantity <= 0)
                    {
                        defenderUnit.Quantity = 0;
                        await this.db.SaveChangesAsync();
                        break;
                    }

                    await this.db.SaveChangesAsync();
                }
            }

            return new Tuple<int, int>(userXpWon, enemyXpWon);
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