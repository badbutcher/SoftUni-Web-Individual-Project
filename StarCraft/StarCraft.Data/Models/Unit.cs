namespace StarCraft.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StarCraft.Data.Models.Enums;
    using static DataConstants;

    public class Unit
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinUnitNameLenght)]
        [MaxLength(MaxUnitNameLenght)]
        public string Name { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(MinMineralUnitCost, MaxMineralUnitCost)]
        public int MineralCost { get; set; }

        [Required]
        [Range(MinGasUnitCost, MaxGasUnitCost)]
        public int GasCost { get; set; }

        [Required]
        [Range(MinUnitHealth, MaxUnitHealth)]
        public int Health { get; set; }

        [Required]
        [Range(MinUnitDamage, MaxUnitDamage)]
        public int Damage { get; set; }

        [Required]
        [MaxLength(MaxByteImageSize)]
        public byte[] Image { get; set; }

        public List<BuildingUnit> Buildings { get; set; } = new List<BuildingUnit>();

        public List<UnitUser> Users { get; set; } = new List<UnitUser>();
    }
}