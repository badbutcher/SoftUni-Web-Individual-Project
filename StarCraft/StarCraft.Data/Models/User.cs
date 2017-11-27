namespace StarCraft.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using StarCraft.Data.Models.Enums;

    public class User : IdentityUser
    {
        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Minerals { get; set; }

        [Required]
        [Range(0, 100000)]
        public int Gas { get; set; }

        public List<UserBuilding> Buildings { get; set; } = new List<UserBuilding>();
    }
}