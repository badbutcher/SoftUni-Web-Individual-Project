namespace StarCraft.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using StarCraft.Data.Models.Enums;

    public class User : IdentityUser
    {
        public Race Race { get; set; }
    }
}