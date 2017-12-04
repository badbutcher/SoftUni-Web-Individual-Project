namespace StarCraft.Web.Models.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StarCraft.Data.Models.Enums;
    using StarCraft.Services.Models;
    using StarCraft.Web.Models.UsersViewModels;

    public class UserBuyUnitsViewModel : UsersResources
    {
        public IEnumerable<BasicUnitInfoServiceModel> Units { get; set; }
    }
}
