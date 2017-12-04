namespace StarCraft.Web.Models.Buildings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using StarCraft.Services.Models;
    using StarCraft.Web.Models.UsersViewModels;

    public class UserBuyBuildingsViewModel : UsersResources
    {
        public IEnumerable<BasicBuildingInfoServiceModel> Buildings { get; set; }
    }
}
