namespace StarCraft.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
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

        public async Task FindRandomPlayer(string userId)
        {
            Random r = new Random();
            User user = await this.db.Users.FirstOrDefaultAsync(a => a.Id == userId);

            var enemies = await this.db.Users
                .Where(a => a.Race != user.Race &&
                a.Level >= user.Level - 1 &&
                a.Level <= user.Level + 1)
                .Take(10)
                .ToListAsync();

            User enemy = enemies[r.Next(0, enemies.Count)];

            List<UnitUser> userUnits = await this.db.Users.Where(a => a.Id == userId).Select(c => c.Units).FirstOrDefaultAsync();
            List<UnitUser> enemyUnits = await this.db.Users.Where(a => a.Id == enemy.Id).Select(c => c.Units).FirstOrDefaultAsync();

            await GetUnits(userUnits);
            await GetUnits(enemyUnits);
            if (userUnits.Count >= enemyUnits.Count)
            {
                StartBattle(userUnits, enemyUnits);
            }
            else if (userUnits.Count < enemyUnits.Count)
            {
                StartBattle(enemyUnits, userUnits);
            }
        }

        private void StartBattle(List<UnitUser> attacker, List<UnitUser> defender)
        {
            int totalAttackerHealth = attacker.Sum(a => a.Unit.Health * a.Quantity);
            int totalAttackerDamage = attacker.Sum(a => a.Unit.Damage * a.Quantity);

            int totalDefenderHealth = defender.Sum(a => a.Unit.Health * a.Quantity);
            int totalDefenderDamage = defender.Sum(a => a.Unit.Damage * a.Quantity);

            //totalAttackerHealth -= totalDefenderDamage;
            //totalDefenderHealth -= totalAttackerDamage;

            foreach (var attackerUnit in attacker)
            {
                int maxHealth = attackerUnit.Unit.Health * attackerUnit.Quantity;
                int maxDmg = attackerUnit.Unit.Damage * attackerUnit.Quantity;
                var currentAttackingUnitHealth = attackerUnit.Unit.Health * attackerUnit.Quantity;
                var currentAttackingUnitDamage = attackerUnit.Unit.Damage * attackerUnit.Quantity;

                foreach (var defenderUnit in defender)
                {
                    var currentDefendingUnitHealth = defenderUnit.Unit.Health * defenderUnit.Quantity;
                    var currentDefendingUnitDamage = defenderUnit.Unit.Damage * defenderUnit.Quantity;

                    currentAttackingUnitHealth -= currentDefendingUnitDamage;
                    currentDefendingUnitHealth -= currentAttackingUnitDamage;

                    attackerUnit.Quantity -= (int)Math.Ceiling((double)currentDefendingUnitDamage / attackerUnit.Unit.Health); /// TODO FIX formula
                    defenderUnit.Quantity -= (int)Math.Ceiling((double)currentAttackingUnitDamage / defenderUnit.Unit.Health); /// TODO FIX formula

                    if (attackerUnit.Quantity <= 0)
                    {
                        attackerUnit.Quantity = 0;
                        break;
                    }
                    else if (defenderUnit.Quantity <= 0)
                    {
                        defenderUnit.Quantity = 0;
                        break;
                    }

                    this.db.SaveChangesAsync();
                }
            }

            Console.WriteLine();
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