namespace StarCraft.Services.Models
{
    using System.ComponentModel.DataAnnotations;
    using StarCraft.Data.Models.Enums;

    public class BasicUnitInfoServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        public int MineralCost { get; set; }

        [Required]
        public int GasCost { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int Damage { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}