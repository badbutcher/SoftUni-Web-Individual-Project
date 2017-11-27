namespace StarCraft.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserBuilding
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }
    }
}
