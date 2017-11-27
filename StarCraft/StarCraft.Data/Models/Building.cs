namespace StarCraft.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using StarCraft.Data.Models.Enums;

    public class Building
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(50, 2000)]
        public int MineralCost { get; set; }

        [Required]
        [Range(0, 2000)]
        public int GasCost { get; set; }

        public List<UserBuilding> Users { get; set; } = new List<UserBuilding>();
    }
}
