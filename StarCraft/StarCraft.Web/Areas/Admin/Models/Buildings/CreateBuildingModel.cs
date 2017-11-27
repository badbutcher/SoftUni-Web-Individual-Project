namespace StarCraft.Web.Areas.Admin.Models.Buildings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;

    public class CreateBuildingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        [Range(50, 2000)]
        [Display(Name = "Mineral Cost")]
        public int MineralCost { get; set; }

        [Required]
        [Range(0, 2000)]
        [Display(Name = "Gas Cost")]
        public int GasCost { get; set; }
    }
}
