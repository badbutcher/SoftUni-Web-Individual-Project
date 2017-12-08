namespace StarCraft.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using static StarCraft.Data.DataConstants;

    public class UnitServiceModel
    {
        [Required]
        [MinLength(MinUnitNameLenght)]
        [MaxLength(MaxUnitNameLenght)]
        public string Name { get; set; }

        [Required]
        [Range(MinItemUnlockLevel, MaxItemUnlockLevel)]
        public int UnlockLevel { get; set; }

        [Required]
        [Range(MinUnitExpWorth, MaxUnitExpWorth)]
        public int ExpWorth { get; set; }

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
    }
}