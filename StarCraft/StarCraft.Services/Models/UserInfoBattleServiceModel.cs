namespace StarCraft.Services.Models
{
    using StarCraft.Data.Models.Enums;

    public class UserInfoBattleServiceModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public int Level { get; set; }

        public Race Race { get; set; }

        public int ArmyQuantity { get; set; }
    }
}
