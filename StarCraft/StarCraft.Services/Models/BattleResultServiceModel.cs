namespace StarCraft.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using StarCraft.Data.Migrations;

    public class BattleResultServiceModel
    {
        public int UserXpWon { get; set; }

        public int EnemyXpWon { get; set; }

        public IList<UnitUser> UserTroopsLost { get; set; }

        public IList<UnitUser> EnemyTroopsLost { get; set; }
    }
}
