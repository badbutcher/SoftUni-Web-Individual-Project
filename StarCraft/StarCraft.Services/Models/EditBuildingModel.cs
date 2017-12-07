namespace StarCraft.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using static StarCraft.Data.DataConstants;

    public class EditBuildingModel
    {
        [Required]
        [MinLength(MinBuildingNameLength)]
        [MaxLength(MaxBuildingNameLength)]
        public string Name { get; set; }

        [Required]
        [Range(MinMineralBuildingCost, MaxMineralBuildingCost)]
        [Display(Name = "Mineral Cost")]
        public int MineralCost { get; set; }

        [Required]
        [Range(MinGasBuildingCost, MaxGasBuildingCost)]
        [Display(Name = "Gas Cost")]
        public int GasCost { get; set; }

        [Required]
        [MaxLength(MaxByteImageSize)]
        [Display(Name = "Building image")]
        public byte[] Image { get; set; }
    }
}