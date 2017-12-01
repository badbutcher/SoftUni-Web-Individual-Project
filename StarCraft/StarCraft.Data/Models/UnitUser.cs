namespace StarCraft.Data.Models
{
    public class UnitUser
    {
        public int UnitId { get; set; }

        public Unit Unit { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}