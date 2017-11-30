namespace StarCraft.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using StarCraft.Data.Models.Enums;
    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(MinUserMineralCapacity, MaxUserMineralCapacity)]
        public int Minerals { get; set; }

        [Required]
        [Range(MinUserGasCapacity, MaxUserGasCapacity)]
        public int Gas { get; set; }

        public List<UserBuilding> Buildings { get; set; } = new List<UserBuilding>();
    }
}