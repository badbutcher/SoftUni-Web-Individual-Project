namespace StarCraft.Web.Areas.Admin.Models.Units
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StarCraft.Data.Models.Enums;
    using static StarCraft.Data.DataConstants;

    public class CreateUnitModel
    {
        [Required]
        [MinLength(MinUnitNameLenght)]
        [MaxLength(MaxUnitNameLenght)]
        public string Name { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(MinMineralUnitCost, MaxMineralUnitCost)]
        [Display(Name = "Mineral Cost")]
        public int MineralCost { get; set; }

        [Required]
        [Range(MinGasUnitCost, MaxGasUnitCost)]
        [Display(Name = "Gas Cost")]
        public int GasCost { get; set; }

        [Required]
        [Range(MinUnitHealth, MaxUnitHealth)]
        [Display(Name = "Unit Health")]
        public int Health { get; set; }

        [Required]
        [Range(MinUnitDamage, MaxUnitDamage)]
        [Display(Name = "Unit Damage")]
        public int Damage { get; set; }

        public string Building { get; set; }

        public Dictionary<Race, List<string>> Buildings { get; set; }

        [Required]
        [MaxLength(MaxByteImageSize)]
        public byte[] Image { get; set; }
    }
}