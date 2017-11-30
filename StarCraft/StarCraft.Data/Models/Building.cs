namespace StarCraft.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using StarCraft.Data.Models.Enums;
    using static DataConstants;

    public class Building
    {
        public int Id { get; set; }

        [Required]
        [MinLength(MinBuildingNameLength)]
        [MaxLength(MaxBuildingNameLength)]
        public string Name { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(MinMineralBuildingCost, MaxMineralBuildingCost)]
        public int MineralCost { get; set; }

        [Required]
        [Range(MinGasBuildingCost, MaxGasBuildingCost)]
        public int GasCost { get; set; }

        [Required]
        [MaxLength(MaxByteImageSize)]
        public byte[] Image { get; set; }

        public List<UserBuilding> Users { get; set; } = new List<UserBuilding>();
    }
}