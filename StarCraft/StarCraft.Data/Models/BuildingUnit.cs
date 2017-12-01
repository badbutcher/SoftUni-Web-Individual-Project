namespace StarCraft.Data.Models
{
    public class BuildingUnit
    {
        public int BuildingId { get; set; }

        public Building Building { get; set; }

        public int UnitId { get; set; }

        public Unit Unit { get; set; }
    }
}