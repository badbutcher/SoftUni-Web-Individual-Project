namespace StarCraft.Data.Models
{
    public class UserBuilding
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }
    }
}