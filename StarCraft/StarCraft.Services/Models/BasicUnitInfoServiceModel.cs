namespace StarCraft.Services.Models
{
    using StarCraft.Data.Models.Enums;

    public class BasicUnitInfoServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Race Race { get; set; }

        public int MineralCost { get; set; }

        public int GasCost { get; set; }

        public int Health { get; set; }

        public int Damage { get; set; }

        public byte[] Image { get; set; }

        public string Quantity { get; set; }
    }
}