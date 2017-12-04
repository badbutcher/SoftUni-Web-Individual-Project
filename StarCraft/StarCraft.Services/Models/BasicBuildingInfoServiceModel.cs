namespace StarCraft.Services.Models
{
    using StarCraft.Data.Models.Enums;

    public class BasicBuildingInfoServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Race Race { get; set; }

        public int MineralCost { get; set; }

        public int GasCost { get; set; }

        public byte[] Image { get; set; }
    }
}