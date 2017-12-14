namespace StarCraft.Services.Models
{
    using System.Collections.Generic;

    public class BattleResultServiceModel
    {
        public int UserMineralsWon { get; set; }

        public int UserGasWon { get; set; }

        public int EnemyMineralsWon { get; set; }

        public int EnemyGasWon { get; set; }

        public int UserXpWon { get; set; }

        public int EnemyXpWon { get; set; }

        public IDictionary<string, int> UserTroopsLost { get; set; } = new Dictionary<string, int>();

        public IDictionary<string, int> EnemyTroopsLost { get; set; } = new Dictionary<string, int>();
    }
}